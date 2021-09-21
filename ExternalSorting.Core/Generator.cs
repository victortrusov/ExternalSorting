using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace ExternalSorting.Core
{
    public class Generator
    {
        /// <summary>
        /// Size of a temporary item collection before writing in bytes
        /// </summary>
        private const int bufferMaxSize = 1 * 1024 * 1024;
        private const int duplicationChance = 10;
        private const int duplicationDivider = 100 / duplicationChance;

        private readonly long size;
        private readonly string path;
        private readonly Random random;

        private readonly Faker<Item> faker;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">Min size of the file in bytes</param>
        /// <param name="path">File path</param>
        public Generator(long size, string path)
        {
            this.path = path;
            this.size = size;
            random = new Random();

            faker = new Faker<Item>()
                .RuleFor(x => x.Number, f => f.Random.Number(0, int.MaxValue))
                .RuleFor(x => x.Text, f => f.Random.Words());
        }

        /// <summary>
        /// Generate file
        /// </summary>
        public async Task Generate()
        {
            var currentSize = 0L;

            using var writer = File.Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

            while (currentSize < size)
            {
                var freeSpace = Math.Min(size - currentSize, bufferMaxSize);

                // I need batches and buffer here because it's bad to use WriteAsync for every line
                var batch = GetBatch(freeSpace);

                await writer.WriteAsync(batch, 0, batch.Length);
                currentSize += batch.Length;
            }
        }

        private byte[] GetBatch(long freeSpace)
        {
            List<byte> buffer = new();
            
            var count = 0;
            Item? itemForDuplication = null;

            while (buffer.Count < freeSpace)
            {
                var item = GetItem(count, ref itemForDuplication);
                var dataBytes = Encoding.UTF8.GetBytes(item.ToString());
                buffer.AddRange(dataBytes);
                count++;
            }

            return buffer.ToArray();
        }

        private Item GetItem(int count, ref Item? itemForDuplication)
        {
            var item = faker.Generate();

            if (count % duplicationDivider == 0)
            {
                itemForDuplication ??= item;

                item = random.Next(2) == 0
                    ? item with { Number = itemForDuplication.Number }
                    : item with { Text = itemForDuplication.Text };

                itemForDuplication = item;
            }

            return item;
        }
    }
}
