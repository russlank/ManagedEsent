﻿//-----------------------------------------------------------------------
// <copyright file="SignatureTests.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace InteropApiTests
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using Microsoft.Isam.Esent.Interop;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for JET_SIGNATURE
    /// </summary>
    [TestClass]
    public class SignatureTests
    {
        /// <summary>
        /// Test constructing a JET_SIGNATURE from a NATIVE_SIGNATURE.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Description("Test constructing a JET_SIGNATURE from a NATIVE_SIGNATURE")]
        public void CreateJetSignatureFromNativeSignature()
        {
            var t = new DateTime(1999, 12, 31, 23, 59, 59, DateTimeKind.Utc);

            var native = new NATIVE_SIGNATURE
            {
                ulRandom = 9,
                logtimeCreate = new JET_LOGTIME(t),
            };

            unsafe
            {
                byte[] name = Encoding.ASCII.GetBytes("COMPUTER");
                Debug.Assert(name.Length < NATIVE_SIGNATURE.ComputerNameSize, "Computer name length is too long");
                for (int i = 0; i < name.Length; ++i)
                {
                    native.szComputerName[i] = name[i];
                }
            }

            var expected = new JET_SIGNATURE(9, t, "COMPUTER");
            var actual = new JET_SIGNATURE(native);
            Assert.AreEqual(expected, actual);
        }
    }
}