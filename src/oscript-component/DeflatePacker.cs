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
    /// Предоставляет методы для упаковки / распаковки данных по алгоритму Deflate
    /// </summary>
    [ContextClass("УпаковщикDeflate", "DeflatePacker")]
    public class DeflatePacker : AutoContext<DeflatePacker>
    {

        /// <summary>
        /// Выполняет упаковку двоичных данных по алгоритму Deflate
        /// </summary>
        /// <param name="InputData">ДвоичныеДанные. Данные для упаковки.</param>
        /// <param name="OutputCompressionLevel">Число. Уровень сжатия (0-2).</param>
        /// <returns>ДвоичныеДанные - Результат упаковки</returns>
        [ContextMethod("УпаковатьДанные")]
        public IValue CompressData(IValue InputData, int OutputCompressionLevel = 2)
        {

            MemoryStreamContext inputStream;

            if (InputData.AsObject() is BinaryDataContext data)
            {
                inputStream = MemoryStreamContext.Constructor();
                data.OpenStreamForRead().CopyTo(inputStream);
                inputStream.Seek(0, StreamPositionEnum.Begin);
            }
            else
            {
                throw RuntimeException.InvalidArgumentType("InputData");
            }

            MemoryStreamContext outputStream = MemoryStreamContext.Constructor();

            CompressStream(inputStream, outputStream);

            return outputStream.CloseAndGetBinaryData();
            
        }

        /// <summary>
        /// Выполняет распаковку данных по алгоритму Deflate
        /// </summary>
        /// <param name="InputData">Поток. Исходный поток для распаковки.</param>
        /// <returns>ДвоичныеДанные - Результат распаковки</returns>
        [ContextMethod("РаспаковатьДанные")]
        public IValue DecompressData(IValue InputData)
        {

            MemoryStreamContext inputStream;

            if (InputData.AsObject() is BinaryDataContext data)
            {
                inputStream = MemoryStreamContext.Constructor();
                data.OpenStreamForRead().CopyTo(inputStream);
                inputStream.Seek(0, StreamPositionEnum.Begin);
            }
            else
            {
                throw RuntimeException.InvalidArgumentType("InputData");
            }

            MemoryStreamContext outputStream = MemoryStreamContext.Constructor();

            DecompressStream(inputStream, outputStream);

            return outputStream.CloseAndGetBinaryData();

        }

        /// <summary>
        /// Выполняет упаковку входящего потока по алгоритму Deflate
        /// </summary>
        /// <param name="InputStream">Поток. Исходный поток для упаковки.</param>
        /// <param name="OutputStream">Поток. Результат упаковки.</param>
        /// <param name="OutputCompressionLevel">Число. Уровень сжатия (0-2).</param>
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
        /// Выполняет распаковку входящего потока по алгоритму Deflate
        /// </summary>
        /// <param name="InputStream">Поток. Исходный поток для распаковки.</param>
        /// <param name="OutputStream">Поток. Результат распаковки.</param>
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
        /// Выполняет упаковку указанного файла по алгоритму Deflate
        /// </summary>
        /// <param name="InputFileName">Строка. Путь к файлу для упаковки.</param>
        /// <param name="OutputFileName">Строка. Путь к файлу - результату упаковки.</param>
        /// <param name="OutputCompressionLevel">Число. Уровень сжатия (0-2).</param>
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
        /// Выполняет распаковку указанного файла по алгоритму Deflate
        /// </summary>
        /// <param name="InputFileName">Строка. Путь к файлу для распаковки.</param>
        /// <param name="OutputFileName">Строка. Путь к файлу - результату распаковки.</param>
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
        /// Создает УпаковщикDeflate
        /// </summary>
        /// <returns>УпаковщикDeflate</returns>
        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            return new DeflatePacker();
        }

    }
}