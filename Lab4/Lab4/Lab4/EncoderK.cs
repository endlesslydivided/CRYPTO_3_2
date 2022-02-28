using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class EncoderK
    {
        public string alphabet;
        public int k;
        public string editedAlphabet;

        public EncoderK(string alphabet, int k)
        {
            this.alphabet = alphabet;
            this.k = k;
            this.editedAlphabet = editAlphabet(alphabet,k);
        }

        public string editAlphabet(string alphabet,int k)
        {
            StringBuilder newAlphabet = new StringBuilder();
            for(int iter = 0; iter < alphabet.Length;iter++)
            {
                newAlphabet.Append(alphabet[(iter + k) % alphabet.Length]);
            }
            return newAlphabet.ToString();
        }

        public string encode(string text)
        {
            StringBuilder encodedText = new StringBuilder();
            for(int iter = 0;iter <text.Length;iter++)
            {
                int pos = this.alphabet.IndexOf(text[iter]);
                char encSymbol = this.editedAlphabet[pos];
                encodedText.Append(encSymbol);
            }
            return encodedText.ToString();
        }

        public string decode(string text)
        {
            StringBuilder decodedText = new StringBuilder();
            for (int iter = 0; iter < text.Length; iter++)
            {
                int pos = this.editedAlphabet.IndexOf(text[iter]);
                char decSymbol = this.alphabet[pos];
                decodedText.Append(decSymbol);
            }
            return decodedText.ToString();
        }
    }
}
