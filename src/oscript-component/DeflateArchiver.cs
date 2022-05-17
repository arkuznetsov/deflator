/*----------------------------------------------------------
Use of this source code is governed by an MIT-style
license that can be found in the LICENSE file or at
https://opensource.org/licenses/MIT.
----------------------------------------------------------
// Codebase: https://github.com/ArKuznetsov/oscript-deflate/
----------------------------------------------------------*/

using System.IO;
using System.IO.Compression;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.HostedScript.Library.Binary;

namespace oscriptcomponent
{
    /// <summary>
    /// АрхиваторDeflate
    /// </summary>
    [ContextClass("АрхиваторDeflate", "DeflateArchiver")]
    public class DeflateArchiver : AutoContext<DeflateArchiver>
    {

        /// <summary>
        /// Упаковать поток
        /// </summary>
        [ContextMethod("УпаковатьПоток")]
        public void CompressStream(IValue InputStream, IValue OutputStream, int OutputCompressionLevel = 2)
        {

            CompressionLevel streamCompressionLevel;

            switch (OutputCompressionLevel)
            {
                case 0:
                    streamCompressionLevel = CompressionLevel.NoCompression;
                    break;
                case 1:
                    streamCompressionLevel = CompressionLevel.Fastest;
                    break;
                case 2:
                    streamCompressionLevel = CompressionLevel.Optimal;
                    break;
                default:
                    streamCompressionLevel = CompressionLevel.Optimal;
                    break;
            }

            DeflateStream compressor;

            if (OutputStream.AsObject() is IStreamWrapper outputStreamWrapper)
            {
                compressor = new DeflateStream(outputStreamWrapper.GetUnderlyingStream(), streamCompressionLevel, true);
            }
            else
            {
                throw RuntimeException.InvalidArgumentType("OutputStream");
            }

            if (InputStream.AsObject() is IStreamWrapper inputStreamWrapper)
            {
                inputStreamWrapper.GetUnderlyingStream().CopyTo(compressor);
                compressor.Close();
            }
            else
            {
                throw RuntimeException.InvalidArgumentType("InputStream");
            }

        }

        /// <summary>
        /// Распаковать поток
        /// </summary>
        [ContextMethod("РаспаковатьПоток")]
        public void DecompressStream(IValue InputStream, IValue OutputStream)
        {

            DeflateStream decompressor;

            if (InputStream.AsObject() is IStreamWrapper inputStreamWrapper)
            {
                decompressor = new DeflateStream(inputStreamWrapper.GetUnderlyingStream(), CompressionMode.Decompress, true);
            }
            else
            {
                throw RuntimeException.InvalidArgumentType("InputStream");
            }

            if (OutputStream.AsObject() is IStreamWrapper outputStreamWrapper)
            {
                decompressor.CopyTo(outputStreamWrapper.GetUnderlyingStream());
                decompressor.Close();
            }
            else
            {
                throw RuntimeException.InvalidArgumentType("OutputStream");
            }

        }

        /// <summary>
        /// Упаковать файл
        /// </summary>
        [ContextMethod("УпаковатьФайл")]
        public void CompressFile(IValue InputFileName, IValue OutputFileName, int OutputCompressionLevel = 2)
        {

            CompressionLevel fileCompressionLevel;

            switch (OutputCompressionLevel)
            {
                case 0:
                    fileCompressionLevel = CompressionLevel.NoCompression;
                    break;
                case 1:
                    fileCompressionLevel = CompressionLevel.Fastest;
                    break;
                case 2:
                    fileCompressionLevel = CompressionLevel.Optimal;
                    break;
                default:
                    fileCompressionLevel = CompressionLevel.Optimal;
                    break;
            }

            FileStream inputFileStream = File.Open(InputFileName.AsString(), FileMode.Open);
            FileStream outputFileStream = File.Create(OutputFileName.AsString());
            var compressor = new DeflateStream(outputFileStream, fileCompressionLevel);
            inputFileStream.CopyTo(compressor);
            compressor.Close();
            inputFileStream.Close();
            outputFileStream.Close();
        }

        /// <summary>
        /// Распаковать файл
        /// </summary>
        [ContextMethod("РаспаковатьФайл")]
        public void DecompressFile(IValue InputFileName, IValue OutputFileName)
        {

            FileStream inputFileStream = File.Open(InputFileName.AsString(), FileMode.Open);
            FileStream outputFileStream = File.Create(OutputFileName.AsString());
            var decompressor = new DeflateStream(inputFileStream, CompressionMode.Decompress);
            decompressor.CopyTo(outputFileStream);
            decompressor.Close();
            inputFileStream.Close();
            outputFileStream.Close();
        }

        /// <summary>
        /// Создает АрхиваторDeflate
        /// </summary>
        /// <returns>АрхиваторDeflate</returns>
        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            return new DeflateArchiver();
        }

    }
}