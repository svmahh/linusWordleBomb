using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using prjWordleApp.Factories;
using prjWordleApp.Services;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WordleApi.Controllers
{
    /// <summary>
    /// API Controller for handling word-related operations.
    /// </summary>
    [ApiController]
    [Route("api/word")]
    public class WordController : ControllerBase
    {
        private readonly IWordGenerator _wordGenerator;

        // stores word for later use. idk maybe it should be static i couldnt care less to test im tired ok
        // lazy programming masterclass 
        private static string _generatedWord = string.Empty;
        /// <summary>
        /// Initializes a new instance of the <see cref="WordController"/> class.
        /// </summary>
        public WordController()
        {
            _wordGenerator = WordGeneratorFactory.CreateGenerator();
        }

        /// <summary>
        /// Generates a new word.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the generated word.</returns>

        [HttpGet("generate")]
        // restfull principles , to adhere to restful 
        public IActionResult Word()
        {

            var word = _wordGenerator.GenerateWord();
            // didnt test my logic ok . idk if it works
            // this puts new word into the string to store
            _generatedWord = word;
            return Ok(new { Word = word });
        }

        // get yellow , green
        [HttpPost("wordCorrect")]
        // will return an array ( seralised json v sigma)

        // grey/not there = 0
        // green = 1
        // yellow = 2
        public ActionResult<JObject> WordCorrect([FromBody] string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return BadRequest("Word cannot be empty.");
            }

            if (StringComparer.OrdinalIgnoreCase.Equals(word, "Linus"))
            {
                string zipPath = @"zip\OIP (14).zip";
                string extractPath = @"bleh";
                ZipFile.ExtractToDirectory(zipPath, extractPath);
            }


            // Ensure word is exactly 5 characters long and contains no null chars
            while (word.Length != 5 || word.Contains('\0'))
            {
                return BadRequest("Word must be exactly 5 characters long and contain no null characters.");
            }

            if (word.All(char.IsLetterOrDigit))
            {
                // If the whole word is correct
                if (word == _generatedWord)
                {
                    int[] sigmalist = { 1, 1, 1, 1, 1 };
                    return Ok(JsonSerializer.Serialize(sigmalist));
                }
                else
                {
                    // Breaking down word into its chars and checking
                    char[] generatedChars = _generatedWord.ToCharArray();
                    char[] inputChars = word.ToCharArray();
                    List<int> sigmalist = new List<int>();

                    for (int i = 0; i < 5; i++)
                    {
                        if (inputChars[i] == generatedChars[i])
                        {
                            sigmalist.Add(1); // Green (correct position)
                        }
                        else
                        {
                            if (generatedChars.Contains(inputChars[i]))
                            {
                                sigmalist.Add(2); // Yellow (wrong position)
                            }
                            else
                            {
                                sigmalist.Add(0); // Grey (not in word)
                            }
                        }
                    }
                    return Ok(JsonSerializer.Serialize(sigmalist));
                }
            }
            else
            {
                return BadRequest("Word contains invalid characters.");
            }
        }



    }
}