﻿using Domain;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IGameRepository
    {
        List<GameSales> GetAll();

        GameDetails GetById(int id);
    }
}
