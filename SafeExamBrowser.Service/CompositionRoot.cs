﻿/*
 * Copyright (c) 2019 ETH Zürich, Educational Development and Technology (LET)
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.IO;
using SafeExamBrowser.Contracts.Logging;
using SafeExamBrowser.Contracts.Service;
using SafeExamBrowser.Logging;

namespace SafeExamBrowser.Service
{
	internal class CompositionRoot
	{
		private ILogger logger;

		internal IServiceController ServiceController { get; private set; }

		internal void BuildObjectGraph()
		{
			logger = new Logger();

			InitializeLogging();

			ServiceController = new ServiceController();
		}

		internal void LogStartupInformation()
		{
			logger.Log($"# Service started at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
			logger.Log(string.Empty);
		}

		internal void LogShutdownInformation()
		{
			logger?.Log($"# Service terminated at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
		}

		private void InitializeLogging()
		{
			var appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nameof(SafeExamBrowser));
			var logFolder = Path.Combine(appDataFolder, "Logs");
			var logFilePrefix = DateTime.Now.ToString("yyyy-MM-dd\\_HH\\hmm\\mss\\s");
			var logFilePath = Path.Combine(logFolder, $"{logFilePrefix}_Service.log");
			var logFileWriter = new LogFileWriter(new DefaultLogFormatter(), logFilePath);

			logFileWriter.Initialize();
			logger.LogLevel = LogLevel.Debug;
			logger.Subscribe(logFileWriter);
		}
	}
}
