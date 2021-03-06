﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace RealNumberApp
{
    public static class Number
    {
        public static bool IsRealNumber(string number)
        {
            var arrayOfChars = number.ToCharArray();
            int step = 1, patternDepth = 1;
            bool isValid = false;

            while (patternDepth <= step && step <= arrayOfChars.Length)
            {
                var part = GetPartOfArray(arrayOfChars, arrayOfChars.Length - step, patternDepth);
                if(IsDefinedInPatterns(part))
                {
                    step++;
                    isValid = true;
                }
                else
                {
                    patternDepth++;
                    isValid = false;
                }
            }

            return isValid;
        }

        private static bool IsDefinedInPatterns(params char[] list) => PatternDefinitions.GetAllowedDefinitions(Parse)
                .Where(pattern => pattern.Count == list.Length)
                .Any(pattern =>
                    {
                        var counter = 0;
                        return pattern.TrueForAll(
                            charType =>
                                PatternDefinitions.CharacterTypeValuesDictionary[charType].IndexOf(list[counter++]) != -1);
                    }
                 );

        private static char[] GetPartOfArray(char[] arrayOfChars, int sourceIndex, int length)
        {
            char[] arrayCopy = new char[length];
            Array.Copy(arrayOfChars, sourceIndex, arrayCopy, 0, length);

            return arrayCopy;
        }

        private static List<CharacterType> Parse(string pattern)
        {
            var patterns = new List<CharacterType>();
            CharacterType patternType;

            while (!string.IsNullOrEmpty(pattern))
            {
                var leftBracketOccurenceIdx = pattern.IndexOf('[');
                if (leftBracketOccurenceIdx != -1)
                {
                    var rightBracketOccurenceIdx = pattern.IndexOf(']');
                    if (rightBracketOccurenceIdx != -1)
                    {
                        var value = pattern.Substring(leftBracketOccurenceIdx + 1, rightBracketOccurenceIdx - 1);
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Enum.TryParse(value, out patternType))
                            {
                                patterns.Add(patternType);
                            }

                            var fragment = pattern.Substring(leftBracketOccurenceIdx, rightBracketOccurenceIdx + 1);
                            int occurence = pattern.IndexOf(fragment);
                            pattern = pattern.Substring(occurence + fragment.Length, pattern.Length - fragment.Length);
                        }
                    }
                }
            }

            return patterns;
        }

        private static class PatternDefinitions
        {
            private static bool isAllowedDefinitionsLoaded;
            private static List<List<CharacterType>> allowedDefinitions;

            public static Dictionary<CharacterType, string> CharacterTypeValuesDictionary { get; } = new Dictionary<CharacterType, string>
            {
                { CharacterType.Dot,  "." },
                { CharacterType.Sign, "-+" },
                { CharacterType.Digit, "1234567890" }
            };

            private static string[] RealNumberAllowedPatterns { get; } = {
                $"[{CharacterType.Digit}]",
                $"[{CharacterType.Dot}][{CharacterType.Digit}]",
                $"[{CharacterType.Sign}][{CharacterType.Digit}]",
                $"[{CharacterType.Digit}][{CharacterType.Dot}][{CharacterType.Digit}]",
                $"[{CharacterType.Sign}][{CharacterType.Dot}][{CharacterType.Digit}]",
                $"[{CharacterType.Sign}][{CharacterType.Digit}][{CharacterType.Dot}][{CharacterType.Digit}]"
            };

            public static List<List<CharacterType>> GetAllowedDefinitions(Func<string, List<CharacterType>> parser)
            {
                if (!isAllowedDefinitionsLoaded)
                {
                    allowedDefinitions = new List<List<CharacterType>>();
                    Array.ForEach(RealNumberAllowedPatterns, pattern => allowedDefinitions.Add(parser(pattern)));
                    isAllowedDefinitionsLoaded = true;
                }

                return allowedDefinitions;
            }
        }

        private enum CharacterType { Digit, Dot, Sign }
    }
}
