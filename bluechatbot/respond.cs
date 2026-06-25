using System.Collections;

namespace bluechatbot
{
    internal class respond
    {
        private ArrayList reply;
        private ArrayList ignore;

        public respond(ArrayList reply, ArrayList ignore)
        {
            this.reply = reply;
            this.ignore = ignore;
        }
    }
}