using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApp.Filter.Base
{
    /// <summary>
    /// Stream wrapper with output processing
    /// </summary>
    public class OutputProcessorStream : Stream
    {
        public OutputProcessorStream(Stream stream, IOutputProcessor processor, Encoding encoding)
        {
            _stream = stream;
            _processor = processor;
            _encoding = encoding;
        }

        private readonly Stream _stream;
        private readonly IOutputProcessor _processor;
        private readonly Encoding _encoding;
        private readonly StringBuilder _data = new StringBuilder();

        public override void Write(byte[] buffer, int offset, int count)
        {
            _data.Append(_encoding.GetString(buffer, offset, count));
        }

        public override void Close()
        {
            var output = _encoding.GetBytes(_processor.Process(_data.ToString()));
            _stream.Write(output, 0, output.Length);
            _stream.Flush();
            _stream.Close();
            _data.Clear();
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override bool CanRead => _stream.CanRead;

        public override bool CanSeek => _stream.CanSeek;
        public override bool CanWrite => _stream.CanWrite;
        public override long Length => _stream.Length;

        public override long Position
        {
            get { return _stream.Position; }
            set { _stream.Position = value; }
        }

    }
}