using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utils;

public class GameMapEntriesViewHelper : MonoBehaviour, IGameMapEntriesViewHelper
{
    [SerializeField]
    private Sprite _defaultMatch;

    [SerializeField]
    private List<GameMapEntryMatch> _spriteMatches;

    public Sprite GetSpriteForEntry(GameMapEntryData entry)
    {
        if(entry.Type == GameMapEntryData.EntryType.Person)
        {
            return Resources.Load<Sprite>(entry.Garrison.Heroes[0].PreviewSpritePath);
        }

        foreach(var match in _spriteMatches)
        {
            if (match.Type != entry.Type)
                continue;

            if (match.CheckFraction)
                if (match.Fraction != entry.Fraction)
                    continue;

            if (match.CheckVariation)
                if (match.Variation != entry.Variation)
                    continue;

            return match.Sprite;
        }
        return _defaultMatch;
    }

    [Serializable]
    private class GameMapEntryMatch
    {
        public GameMapEntryData.EntryType Type;

        public bool CheckFraction = false;
        public FractionType Fraction;

        public bool CheckVariation = false;
        public int Variation;

        public Sprite Sprite;
    }
}
