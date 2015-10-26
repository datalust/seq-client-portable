using System;
using System.Net.Http;
using Seq.Client.Portable;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace Seq
{
    /// <summary>
    /// Extends Serilog configuration to write events to Seq.
    /// </summary>
    public static class SeqPortableLoggerConfigurationExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events to a http://getseq.net Seq event server.
        /// </summary>
        /// <param name="loggerSinkConfiguration">The logger configuration.</param>
        /// <param name="serverUrl">The base URL of the Seq server that log events will be written to.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required 
        /// in order to write an event to the sink.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="apiKey">A Seq <i>API key</i> that authenticates the client to the Seq server.</param>
        /// <param name="httpClientFactory">A custom factory that will be used to obtain an <see cref="HttpClient"/> instance.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration SeqPortable(
            this LoggerSinkConfiguration loggerSinkConfiguration,
            string serverUrl,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = PortableSeqSink.DefaultBatchPostingLimit,
            TimeSpan? period = null,
            string apiKey = null, IHttpClientFactory httpClientFactory = null)
        {
            if (loggerSinkConfiguration == null) throw new ArgumentNullException("loggerSinkConfiguration");
            if (serverUrl == null) throw new ArgumentNullException("serverUrl");

            var defaultedPeriod = period ?? PortableSeqSink.DefaultPeriod;
            httpClientFactory = httpClientFactory ?? new DefaultHttpClientFactory();

            return loggerSinkConfiguration.Sink(
                new PortableSeqSink(serverUrl, apiKey, batchPostingLimit, defaultedPeriod, httpClientFactory),
                restrictedToMinimumLevel);
        }
    }
}
