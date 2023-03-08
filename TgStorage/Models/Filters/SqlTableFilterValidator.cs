// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TgStorage.Models.Filters;

[DebuggerDisplay("{nameof(SqlTableFilterValidator)}")]
public class SqlTableFilterValidator : SqlTableXpLiteValidatorBase<SqlTableFilterModel>
{
	#region Public and private fields, properties, constructor

	public SqlTableFilterValidator()
	{
		RuleFor(item => item.FilterType)
			.NotEmpty()
			.NotNull();
		RuleFor(item => item.Name)
			.NotEmpty()
			.NotNull();
		RuleFor(item => item.Mask)
			.NotNull();
		RuleFor(item => item.Size)
			.NotNull();
		RuleFor(item => item.SizeType)
			.NotNull();
	}

	#endregion
}