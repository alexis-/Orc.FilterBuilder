﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BooleanExpression.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.FilterBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq.Expressions;

    using Catel;
    using Catel.Runtime.Serialization;

    using Orc.FilterBuilder.Models;

    [DebuggerDisplay("{ValueControlType} {SelectedCondition} {Value}")]
    public class BooleanExpression : DataTypeExpression
    {
        #region Constructors
        public BooleanExpression()
        {
            BooleanValues = new List<bool> { true, false };
            Value = true;
            SelectedCondition = Condition.EqualTo;
            ValueControlType = ValueControlType.Boolean;
        }
        #endregion



        #region Properties
        public bool Value { get; set; }

        [ExcludeFromSerialization]
        public List<bool> BooleanValues { get; set; }
        #endregion



        #region Methods
        public override bool CalculateResult(IPropertyMetadata propertyMetadata, object entity)
        {
            bool entityValue = propertyMetadata.GetValue<bool>(entity);
            switch (SelectedCondition)
            {
                case Condition.EqualTo:
                    return entityValue == Value;

                default:
                    throw new NotSupportedException(string.Format("Condition '{0}' is not supported.", SelectedCondition));
            }
        }

        /// <summary>
        ///   Converts <see cref="ConditionTreeItem"/> to a LINQ <see cref="Expression"/>
        /// </summary>
        /// <param name="propertyExpr">LINQ <see cref="MemberExpression"/>.</param>
        /// <returns>LINQ Expression.</returns>
        public override Expression ToLinqExpression(Expression propertyExpr)
        {
            Argument.IsNotNull(() => propertyExpr);

            var valueExpr = Expression.Constant(Value, typeof(bool));

            return Expression.Equal(propertyExpr, valueExpr);
        }

        public override object Clone()
        {
            return new BooleanExpression { Value = this.Value, SelectedCondition = this.SelectedCondition };
        }

        public override string ToString()
        {
            return string.Format("{0} '{1}'", SelectedCondition.Humanize(), Value);
        }
        #endregion
    }
}