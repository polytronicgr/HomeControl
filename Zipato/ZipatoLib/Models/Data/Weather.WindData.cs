﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Weather.WindData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Data.Weather
{
    public class WindData
    {
        public double? mph { get; set; }
        public double? kph { get; set; }
        public string Dir { get; set; }
        public double? Degrees { get; set; }
    }
}
