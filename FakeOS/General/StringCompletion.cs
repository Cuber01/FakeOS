using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeOS.General;

public class StringCompletion
{
    private readonly List<string> keys;

    public StringCompletion(List<string> keys)
    {
        this.keys = keys;
    }

    public string complete(string text)
    {
        if (keys.Count == 0) throw new Exception("No keys for autocompletion provided.");
        if (text.Length == 0) return text;

        List<string> matches = new List<string>(keys);
        
        for (int i = 0; i < text.Length; i++)
        {
            foreach (var key in keys)
            {
                // If the input text is longer than a key, unmatch it
                if (key.Length < text.Length)
                {
                    matches.Remove(key);
                    continue;
                }

                // If a char on the same position on key and text are different, unmatch the key
                if (key[i] != text[i])
                {
                    matches.Remove(key);
                }
            }
        }

        // Predict as much as we can
        if (matches.Count != 1)
        {
            StringBuilder sameString = new StringBuilder();
            sameString.Append(matches.ElementAt(0)[0]);

            // Iterate through each character of a match entry
            foreach (var match in matches)
            {
                for (int i = 0; i < match.Length; i++)
                {
                    
                    if (i > sameString.Length - 1 || sameString[i] != match[i])
                    {
                        bool add = doWeAdd((match[i], i), matches);

                        // We abort on failure, so we won't end up with a situation where:
                        // input = mk
                        // matches = mkfile, mkdir
                        //
                        // Hey look i in mkfile and mkdir are at the same place, who cares if their third character is different,
                        // let's add it!
                        //
                        // result: mkii
                        if (!add)
                        {
                            break;
                        }
                        
                        sameString.Append(match[i]);
                        
                    }

                }
                
            }
            
            return sameString.ToString();
        }
        else
        {
            return matches.ElementAt(0) + ' ';
        }

    }
    
    private bool doWeAdd((char, int) charAtIndex, List<string> matches)
    {
        bool rv = true;
            
        foreach (var entry in matches)
        {
            
            // If the matches contradict themselves, we do not add the character
            if (charAtIndex.Item2 > entry.Length - 1 || entry[charAtIndex.Item2] != charAtIndex.Item1)
            {
                rv = false;
            }
            
        }

        return rv;
    }
}