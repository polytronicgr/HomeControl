﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="bitfield16.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
// Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace SunSpecLib
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using Newtonsoft.Json;
    using NModbus.Extensions;
    using SunSpecLib.Converters;

    #endregion

    /// <summary>
    /// Sunspec 16-bit integer values: bitfield16
    /// Range: ­0...0x7FFF
    /// Not Implemented: 0xFFFF
    /// </summary>
    [JsonConverter(typeof(Bitfield16JsonConverter))]
    [TypeConverter(typeof(Bitfield16TypeConverter))]
    public struct bitfield16
    {
        public const ushort NOT_IMPLEMENTED = 0xFFFF;
        public const ushort MIN_VALUE = 0x0000;
        public const ushort MAX_VALUE = 0x7FFF;

        private ushort _value;

        [JsonIgnore]
        public bool NotImplemented { get => _value == NOT_IMPLEMENTED; }

        public static implicit operator bitfield16(ushort[] registers)
        {
            if (registers == null)
            {
                throw new ArgumentNullException("The register array is null.", nameof(registers));
            }

            if (registers.Length < 1)
            {
                throw new ArgumentOutOfRangeException("The register array does not contain enough elements.", nameof(registers));
            }

            return new bitfield16 { _value = registers[0] };
        }

        public static implicit operator bitfield16(ushort value)
            => new bitfield16 { _value = ((value >= MIN_VALUE) && (value <= MAX_VALUE)) ? value : NOT_IMPLEMENTED };

        public static implicit operator ushort(bitfield16 value)
            => value._value;

        public ushort[] ToRegisters()
            => ModbusConversions.ToRegisters(_value);

        public override string ToString()
            => _value.ToString();
    }
}
