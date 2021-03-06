﻿/*
 * Copyright (c) 2019 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

namespace SafeExamBrowser.Contracts.Configuration.Settings
{
	/// <summary>
	/// Defines all configuration options for the <see cref="Monitoring.IMouseInterceptor"/>.
	/// </summary>
	[Serializable]
	public class MouseSettings
	{
		/// <summary>
		/// Determines whether the user may use the middle mouse button.
		/// </summary>
		public bool AllowMiddleButton { get; set; }

		/// <summary>
		/// Determines whether the user may use the right mouse button.
		/// </summary>
		public bool AllowRightButton { get; set; }
	}
}
