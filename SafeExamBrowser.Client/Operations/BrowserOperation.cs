﻿/*
 * Copyright (c) 2019 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using SafeExamBrowser.Contracts.Applications;
using SafeExamBrowser.Contracts.Core.OperationModel;
using SafeExamBrowser.Contracts.Core.OperationModel.Events;
using SafeExamBrowser.Contracts.I18n;
using SafeExamBrowser.Contracts.Logging;
using SafeExamBrowser.Contracts.UserInterface;
using SafeExamBrowser.Contracts.UserInterface.Shell;

namespace SafeExamBrowser.Client.Operations
{
	internal class BrowserOperation : IOperation
	{
		private IActionCenter actionCenter;
		private IApplicationController browserController;
		private IApplicationInfo browserInfo;
		private ILogger logger;
		private ITaskbar taskbar;
		private IUserInterfaceFactory uiFactory;

		public event ActionRequiredEventHandler ActionRequired { add { } remove { } }
		public event StatusChangedEventHandler StatusChanged;

		public BrowserOperation(
			IActionCenter actionCenter,
			IApplicationController browserController,
			IApplicationInfo browserInfo,
			ILogger logger,
			ITaskbar taskbar,
			IUserInterfaceFactory uiFactory)
		{
			this.actionCenter = actionCenter;
			this.browserController = browserController;
			this.browserInfo = browserInfo;
			this.logger = logger;
			this.taskbar = taskbar;
			this.uiFactory = uiFactory;
		}

		public OperationResult Perform()
		{
			logger.Info("Initializing browser...");
			StatusChanged?.Invoke(TextKey.OperationStatus_InitializeBrowser);

			var actionCenterControl = uiFactory.CreateApplicationControl(browserInfo, Location.ActionCenter);
			var taskbarControl = uiFactory.CreateApplicationControl(browserInfo, Location.Taskbar);

			browserController.Initialize();
			browserController.RegisterApplicationControl(actionCenterControl);
			browserController.RegisterApplicationControl(taskbarControl);

			actionCenter.AddApplicationControl(actionCenterControl);
			taskbar.AddApplicationControl(taskbarControl);

			return OperationResult.Success;
		}

		public OperationResult Revert()
		{
			logger.Info("Terminating browser...");
			StatusChanged?.Invoke(TextKey.OperationStatus_TerminateBrowser);

			browserController.Terminate();

			return OperationResult.Success;
		}
	}
}
