﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderData.cs" company="DTV-Online">
//   Copyright(c) 2019 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace HomeControlLib.Fronius.Models
{
    #region Using Directives

    using System;

    #endregion

    public class HeaderData
    {
        public RequestArgumentsData RequestArguments { get; set; } = new RequestArgumentsData();
        public StatusData Status { get; set; } = new StatusData();
        public DateTime Timestamp { get; set; } = new DateTime();
    }
}
