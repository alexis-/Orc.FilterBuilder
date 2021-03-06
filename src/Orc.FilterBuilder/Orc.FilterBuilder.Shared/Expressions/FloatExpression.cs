﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FloatExpression.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.FilterBuilder
{
    using System;
    using System.Diagnostics;
    using Models;

    [DebuggerDisplay("{ValueControlType} {SelectedCondition} {Value}")]
    public class FloatExpression : NumericExpression<float>
    {
        #region Constructors
        public FloatExpression()
            : this(true)
        {
        }

        public FloatExpression(bool isNullable)
        {
            IsDecimal = true;
            IsNullable = isNullable;
            IsSigned = true;
            ValueControlType = ValueControlType.Float;
        }
        #endregion



        public override object Clone()
        {
            return new FloatExpression(IsNullable) { Value = this.Value, SelectedCondition = this.SelectedCondition };
        }
    }
}
