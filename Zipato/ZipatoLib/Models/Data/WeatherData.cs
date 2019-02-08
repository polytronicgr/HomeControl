﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeatherData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace ZipatoLib.Models.Data
{
    #region Using Directives

    using System.Collections.Generic;

    using ZipatoLib.Models.Data.Weather;

    #endregion

    public class WeatherData
    {
        public ConditionData Condition { get; set; }
        public AstronomyData Astronomy { get; set; }
        public List<ForecastData> Forecast { get; set; } = new List<ForecastData> { };
    }
}
