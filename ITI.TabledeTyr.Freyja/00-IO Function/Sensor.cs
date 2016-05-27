﻿using ITI.GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TabledeTyr.Freyja
{
    class Sensor
    {
        Freyja_Core _ctx;
        internal Game _game;
        internal bool _isAtkplaying;
        internal readonly bool _isFreyjaAtk;
        internal IReadOnlyTafl currentTafl;
        internal ITafl convertedTafl;

        public Sensor(Freyja_Core ctx, Game game, bool isFreyjaAtk)
        {
            _ctx = ctx;
            _game = game;
            _isFreyjaAtk = isFreyjaAtk;
            _isAtkplaying = game.IsAtkPlaying;
            currentTafl = game.Tafl;

        }
        internal TaflCompressed ConvertTaflBasicIntoCompressed (TaflBasic source)
        {
            return new TaflCompressed(source);
        }
        internal TaflBasic ConvertTaflCompressedIntoBasic(TaflCompressed source)
        {
            return new TaflBasic(source);
        }
        public void updateSensor(Game newGameTurn)
        {
            _game = newGameTurn;
            _isAtkplaying = newGameTurn.IsAtkPlaying;
            currentTafl = newGameTurn.Tafl;
        }

    }
}
