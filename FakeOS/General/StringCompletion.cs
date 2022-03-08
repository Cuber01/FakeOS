using System;
using System.Collections.Generic;
using System.Linq;

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

        // If there are no matches, or more than one, don't change anything
        if (matches.Count != 1)
        {
            return text;
        }
        else
        {
            return matches.ElementAt(0);
        }
        
    }
}