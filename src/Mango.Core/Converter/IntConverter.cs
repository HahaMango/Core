﻿using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mango.Core.Converter
{
    /// <summary>
    /// 整型JSON转换器
    /// </summary>
    public class IntConverter : JsonConverter<int>
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
            if (reader.TokenType == JsonTokenType.String)
            {
                if (Utf8Parser.TryParse(span, out int result1, out int length) && length == span.Length)
                {
                    return result1;
                }
                if (int.TryParse(reader.GetString(), out int result2))
                {
                    return result2;
                }
            }
            return 0;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
