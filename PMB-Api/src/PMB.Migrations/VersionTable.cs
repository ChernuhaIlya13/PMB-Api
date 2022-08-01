using FluentMigrator.Runner.VersionTableInfo;

namespace PMB.Migrations
{
    public class VersionTable : IVersionTableMetaData
    {
        public object ApplicationContext { get; set; }

        public bool OwnsSchema => true;

        public string SchemaName => "public";

        public string TableName => "version_info";

        public string ColumnName => "version";

        public string DescriptionColumnName => "description";

        public string AppliedOnColumnName => "applied_on";

        public string UniqueIndexName => "uc_version";
    }
}
