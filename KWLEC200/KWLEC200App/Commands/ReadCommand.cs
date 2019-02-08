﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadCommand.cs" company="DTV-Online">
//   Copyright(c) 2018 Dr. Peter Trimmel. All rights reserved.
// </copyright>
// <license>
//   Licensed under the MIT license. See the LICENSE file in the project root for more information.
// </license>
// --------------------------------------------------------------------------------------------------------------------
namespace KWLEC200App.Commands
{
    #region Using Directives

    using System;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json;

    using CommandLine.Core.Hosting.Abstractions;

    using BaseClassLib;
    using KWLEC200Lib;
    using KWLEC200Lib.Models;
    using KWLEC200App.Models;

    #endregion

    /// <summary>
    /// Application command "read".
    /// </summary>
    [Command(Name = "read",
             FullName = "KWLEC200 Read Command",
             Description = "Reading data values from Helios KWL EC200 ventilation unit.",
             ExtendedHelpText = "\nCopyright (c) 2018 Dr. Peter Trimmel - All rights reserved.")]
    [HelpOption("-?|--help")]
    public class ReadCommand : BaseCommand<AppSettings>
    {
        #region Private Data Members

        private readonly IKWLEC200 _kwlec200;

        #endregion

        #region Private Properties

        /// <summary>
        /// This is a reference to the parent command <see cref="RootCommand"/>.
        /// </summary>
        private RootCommand Parent { get; set; }

        #endregion

        #region Public Properties

        [Argument(0, "Reads the named property.")]
        public string Property { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCommand"/> class.
        /// </summary>
        /// <param name="kwlec200">The KWLEC200 instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The setting options instance.</param>
        /// <param name="environment">The hosting environment instance.</param>
        public ReadCommand(IKWLEC200 kwlec200,
                           ILogger<InfoCommand> logger,
                           IOptions<AppSettings> options,
                           IHostingEnvironment environment)
            : base(logger, options, environment)
        {
            _logger?.LogDebug("ReadCommand()");

            // Setting the KWLEC200 instance.
            _kwlec200 = kwlec200;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to run when command is executed.
        /// </summary>
        /// <returns>Zero if ok.</returns>
        public int OnExecute(CommandLineApplication app)
        {
            try
            {
                if (CheckOptions(app))
                {
                    // Overriding KWLEC200 options.
                    _kwlec200.Slave.Address = Parent.Address;
                    _kwlec200.Slave.Port = Parent.Port;
                    _kwlec200.Slave.ID = Parent.SlaveID;

                    if (Property.Length == 0)
                    {
                        Console.WriteLine($"Reading all data from KWLEC200 hvac.");
                        var status = _kwlec200.ReadAll();

                        if (status.IsGood)
                        {
                            Console.WriteLine($"KWLEC200: {JsonConvert.SerializeObject(_kwlec200, Formatting.Indented)}");
                        }
                        else
                        {
                            Console.WriteLine($"Error reading data from KWLEC200 hvac.");
                            Console.WriteLine($"Reason: {status.Explanation}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Reading property '{Property}' from KWLEC200 _kwlec200.");
                        var status = _kwlec200.ReadData(Property);

                        if (status.IsGood)
                        {
                            Console.WriteLine($"Value of property '{Property}' = {_kwlec200.Data.GetPropertyValue(Property)}");
                        }
                        else
                        {
                            Console.WriteLine($"Error reading property '{Property}' from KWLEC200 hvac.");
                            Console.WriteLine($"Reason: {status.Explanation}.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Exception ReadCommand.");
                return -1;
            }

            return 0;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Helper method to check options.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>True if options are OK.</returns>
        private bool CheckOptions(CommandLineApplication app)
        {
            if (Property.Length > 0)
            {
                if (!KWLEC200Data.IsProperty(Property))
                {
                    _logger?.LogError($"The property '{Property}' has not been found.");
                    return false;
                }

                if (!KWLEC200Data.IsReadable(Property))
                {
                    _logger?.LogError($"The property '{Property}' is not readable.");
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
