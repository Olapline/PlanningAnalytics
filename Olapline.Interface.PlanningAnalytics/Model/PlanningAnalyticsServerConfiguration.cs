using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olapline.Interface.PlanningAnalytics.Model
{

    public class PlanningAnalyticsServerConfiguration
    {
        public String ServerName;
        public String AdminHost;
        public String ProductVersion;
        public int PortNumber;
        public int ClientMessagePortNumber;
        public int HTTPPortNumber;
        public bool IntegratedSecurityMode;
        public String SecurityMode;
        public String PrincipalName;
        public String SecurityPackageName;
        public List<String> ClientCAMURIs;
        public String WebCAMURI;
        public String ClientPingCAMPassport;
        public String ServerCAMURI;
        public bool AllowSeparateNandCRules;
        public String DistributedOutputDir;
        public bool DisableSandboxing;
        public bool JobQueuing;
        public bool ForceReevaluationOfFeedersForFedCellsOnDataChange;
        public String DataBaseDirectory;
        public bool UnicodeUpperLowerCase;
    }


    public class ServerSettings
    {
        public String ServerName { get; set; }

        public AccessSettings Access { get; set; }

        public AdministrationSettings Administration { get; set; }
        public ModellingSettings Modelling { get; set; }

        public PerformanceSettings Performance { get; set; }
    }


    public class AccessSettings
    {
        public NetworkSettings Network { get; set; }

        public AuthenticationSettings Authentication { get; set; }
        public SSLSettings SSL { get; set; }

        public CAMSettings CAM { get; set; }
        public LDAPSettings LDAP { get; set; }
        public CAPISettings CAPI { get; set; }
        public HTTPSettings HTTP { get; set; }
    }

    public class NetworkSettings
    {
        public String IPAddress { get; set; }
        public IPVersion IPVersion { get; set; }

        public String NetRecvBlockingWaitLimit { get; set; }

        public String NetRecvMaxClientIOWaitWithinAPIs { get; set; }
        public String IdleConnectionTimeOut { get; set; }
        public String ReceiveProgressResponseTimeout { get; set; }
    }

    public class AuthenticationSettings
    {
        public String SecurityPackageName { get; set; }
        public String ServicePrincipalName { get; set; }
        public SecurityMode IntegratedSecurityMode { get; set; }
        public int MaximumLoginAttempts { get; set; }
    }

    public class SSLSettings
    {
        public bool Enable { get; set; }
        public bool ClientExportServerCertificate { get; set; }
        public String CertificateID { get; set; }
        public String CertificateFile { get; set; }
        public String PrivateKeyPwdFile { get; set; }
        public String PwdKeyFile { get; set; }
        public String CertAuthority { get; set; }
        public String CertRevocationFile { get; set; }
        public String ClientExportServerKeyID { get; set; }
        public String KeyFile { get; set; }
        public String KeyStashFile { get; set; }
        public String KeyLabel { get; set; }
        public String TLSCipherList { get; set; }
        public FIPSMode FIPSOperationMode { get; set; }
        public bool NIST_SP800_131A_MODE { get; set; }
    }

    public class CAMSettings
    {
        public bool CAMUseSSL { get; set; }
        public String ClientURI { get; set; }
        public List<String> ServerURIs { get; set; }
        public String SSLCertificate { get; set; }
        public String PortalVariableFile { get; set; }
        public String ClientPingCAMPassport { get; set; }
        public int ServerCAMURIRetryAttempts { get; set; }
        public bool CreateNewCAMClients { get; set; }
    }

    public class LDAPSettings
    {

        public bool Enable { get; set; }
        public String Host { get; set; }
        public int Port { get; set; }
        public bool UseServerAccount { get; set; }
        public List<String> VerifyCertServerName { get; set; }
        public bool VerifyServerSSLCert { get; set; }
        public bool SkipSSLCertVerification { get; set; }
        public bool SkipSSLCRLVerification { get; set; }
        public String WellKnownUserName { get; set; }
        public String PasswordFile { get; set; }
        public String PasswordKeyFile { get; set; }
        public String SearchBase { get; set; }
        public String SearchField { get; set; }

    }

    public class CAPISettings
    {
        public int Port { get; set; }
        public int ClientMessagePort { get; set; }
        public bool MessageCompression { get; set; }
        public bool ProgressMessage { get; set; }
        public int ClientVersionMaximum { get; set; }
        public int ClientVersionMinimum { get; set; }
        public int ClientVersionPrecision { get; set; }

    }

    public class AdministrationSettings
    {
        public String ServerName { get; set; }
        public String AdminHost { get; set; }
        public String Language { get; set; }
        public String DataBaseDirectory { get; set; }
        public bool UnicodeUpperLowerCase { get; set; }
        public bool MaskUserNameInServerTools { get; set; }
        public bool AllowReadOnlyChoreReschedule { get; set; }
        public bool DisableSandboxing { get; set; }

        [JsonConverter(typeof(BooleanConverter))]
        public bool RunningInBackground { get; set; }
        public List<String> StartupChores { get; set; }
        public bool PerformanceMonitorOn { get; set; }
        public bool PerfMonActive { get; set; }
        public bool EnableSandboxDimension { get; set; }
        public ClientSettings Clients { get; set; }
        public AuditLogSettings AuditLog { get; set; }
        public DebugLogSettings DebugLog { get; set; }
        public ServerLogSettings ServerLog { get; set; }
        public JavaSettings Java { get; set; }
        public ExternalDatabaseSettings ExternalDatabase { get; set; }
        public TM1WebSettings TM1Web { get; set; }
    }




    public class HTTPSettings
    {
        public int Port { get; set; }
        public String SessionTimeout { get; set; }
    }

    public class ClientSettings
    {
        public int PasswordMinimumLength { get; set; }
        public String ClientPropertiesSyncInterval { get; set; }

        //[JsonConverter(typeof(BooleanConverter))]
        //public bool RetainNonCAMGroupMembership { get; set; }
    }

    public class AuditLogSettings
    {
        public bool Enable { get; set; }
        public String UpdateInterval { get; set; }
        public Int64 MaxFileSizeKilobytes { get; set; }
        public Int64 MaxQueryMemoryKilobytes { get; set; }
    }


    public class DebugLogSettings
    {
        public String LoggingDirectory { get; set; }
    }

    public class ServerLogSettings
    {
        public bool Enable { get; set; }
        public int LogReleaseLineCount { get; set; }

    }

    public class JavaSettings
    {
        public String ClassPath { get; set; }
        public String JVMPath { get; set; }
        public String JVMArgs { get; set; }
    }


    public class ExternalDatabaseSettings
    {
        //public OracleErrorForceRowStatus OracleErrorForceRowStatus { get; set; }

        //[JsonConverter(typeof(SQLFetchTypeConverter))]
        //public SQLFetchType SQLFetchType { get; set; }
        public String ODBCLibraryPath { get; set; }
        public bool TM1ConnectorforSAP { get; set; }
        public bool UseNewConnectorforSAP { get; set; }
    }

    public class TM1WebSettings
    {
        public bool ExcelWebPublishEnabled { get; set; }
    }

    public class ModellingSettings
    {
        public bool DefaultMeasuresDimension { get; set; }
        public bool UserDefinedCalculations { get; set; }
        public bool EnableNewHierarchyCreation { get; set; }
        public SpreadingSettings Spreading { get; set; }
        public TISettings TI { get; set; }
        public RulesSettings Rules { get; set; }
        public StartupSettings Startup { get; set; }
        public SynchronizationSettings Synchronization { get; set; }
    }

    public class SpreadingSettings
    {
        public Double SpreadingPrecision { get; set; }
        public bool ProportionSpreadToZeroCells { get; set; }
    }

    public class TISettings
    {
        public String CognosTM1InterfacePath { get; set; }
        public bool UseExcelSerialDate { get; set; }
        public int MaximumTILockObjects { get; set; }
        public bool EnableTIDebugging { get; set; }
    }

    public class RulesSettings
    {
        public bool AllowSeparateNandCRules { get; set; }
        public bool AutomaticallyAddCubeDependencies { get; set; }
        public bool RulesOverwriteCellsOnLoad { get; set; }
        public bool ForceReevaluationOfFeedersForFedCellsOnDataChange { get; set; }
    }

    public class StartupSettings
    {
        public bool PersistentFeeders { get; set; }
        public bool SkipLoadingAliases { get; set; }
        public int MaximumCubeLoadThreads { get; set; }
    }


    public class SynchronizationSettings
    {
        public int SyncUnitSize { get; set; }
        public int MaximumSynchAttempts { get; set; }
    }

    public class PerformanceSettings
    {
        public MemorySettings Memory { get; set; }
        public MTQSettings MTQ { get; set; }
        public LockingSettings Locking { get; set; }
        public ViewCalculationSettings ViewCalculation { get; set; }
        public StargateSettings Stargate { get; set; }
        public JobQueuingSettings JobQueuing { get; set; }

    }

    public class MemorySettings
    {
        public bool ApplyMaximumViewSizeToEntireTransaction { get; set; }
        public bool DisableMemoryCache { get; set; }
        public bool CacheFriendlyMalloc { get; set; }
        public Int64 MaximumViewSizeMB { get; set; }
        public Int64 MaximumUserSandboxSizeMB { get; set; }
        public Int64 MaximumMemoryForSubsetUndoKB { get; set; }
        public bool LockPagesInMemory { get; set; }
    }

    public class MTQSettings
    {
        public bool UseAllThreads { get; set; }
        public int NumberOfThreadsToUse { get; set; }
        public bool SingleCellConsolidation { get; set; }
        public bool ImmediateCheckForSplit { get; set; }
        public Int64 OperationProgressCheckSkipLoopSize { get; set; }

    }

    public class LockingSettings
    {
        public bool SubsetElementLockBreathing { get; set; }
        public bool UseLocalCopiesForPublicDynamicSubsets { get; set; }
    }

    public class ViewCalculationSettings
    {
        public int MagnitudeDifferenceToBeZero { get; set; }
        public Int64 CheckFeedersMaximumCells { get; set; }
        public Int64 CalculationThresholdForStorage { get; set; }
        public bool ViewConsolidationOptimization { get; set; }
        public String ViewConsolidationOptimizationMethod { get; set; }

    }

    public class StargateSettings
    {
        public bool ZeroWeightOptimization { get; set; }
        public bool AllRuleCalcStargateOptimization { get; set; }
        public bool UseStargateForRules { get; set; }

    }

    public class JobQueuingSettings
    {
        public bool Enable { get; set; }
        public String ThreadSleepTime { get; set; }
        public int ThreadPoolSize { get; set; }
        public String MaxWaitTime { get; set; }
    }
}
