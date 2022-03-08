using System;
using System.Collections.Generic;
using System.Linq;

namespace FakeOS.General;

public class StringCompletion
{
    public List<string> keys;

    public StringCompletion()
    {
        
    }

    public string complete(string text)
    {
        if (keys.Count == 0) throw new Exception("No keys for autocompletion provided.");

        List<string> matches = keys;
        
        for (int i = 0; i <= text.Length; i++)
        {
            foreach (var key in keys)
            {
                
                if (key[i] != text[i])
                {
                    matches.Remove(key);
                }
                
            }
        }

        return matches.ElementAt(0);
    }
}