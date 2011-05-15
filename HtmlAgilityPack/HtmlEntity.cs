// HtmlAgilityPack V1.0 - Simon Mourier <simon underscore mourier at hotmail dot com>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HtmlAgilityPack
{
    using System.Linq;

    /// <summary>
    /// A utility class to replace special characters by entities and vice-versa.
    /// Follows HTML 4.0 specification found at http://www.w3.org/TR/html4/sgml/entities.html
    /// </summary>
    public static class HtmlEntity
    {
        #region Static Members

        private static readonly int _maxEntitySize;
        private static Dictionary<int,string> _entityName;
        private static readonly Dictionary<string, int> _entityValue;

        /// <summary>
        /// A collection of entities indexed by name.
        /// </summary>
        public static Dictionary<int, string> EntityName
        {
            get { return _entityName; }
        }

        /// <summary>
        /// A collection of entities indexed by value.
        /// </summary>
        public static Dictionary<string, int> EntityValue
        {
            get { return _entityValue; }
        }

        #endregion

        static HtmlEntity()
        {
            _entityName =
                new Dictionary<int, string>
                    {
                        {160, "nbsp"},
                        {161, "iexcl"},
                        {162, "cent"},
                        {163, "pound"},
                        {164, "curren"},
                        {165, "yen"},
                        {166, "brvbar"},
                        {167, "sect"},
                        {168, "uml"},
                        {169, "copy"},
                        {170, "ordf"},
                        {171, "laquo"},
                        {172, "not"},
                        {173, "shy"},
                        {174, "reg"},
                        {175, "macr"},
                        {176, "deg"},
                        {177, "plusmn"},
                        {178, "sup2"},
                        {179, "sup3"},
                        {180, "acute"},
                        {181, "micro"},
                        {182, "para"},
                        {183, "middot"},
                        {184, "cedil"},
                        {185, "sup1"},
                        {186, "ordm"},
                        {187, "raquo"},
                        {188, "frac14"},
                        {189, "frac12"},
                        {190, "frac34"},
                        {191, "iquest"},
                        {192, "Agrave"},
                        {193, "Aacute"},
                        {194, "Acirc"},
                        {195, "Atilde"},
                        {196, "Auml"},
                        {197, "Aring"},
                        {198, "AElig"},
                        {199, "Ccedil"},
                        {200, "Egrave"},
                        {201, "Eacute"},
                        {202, "Ecirc"},
                        {203, "Euml"},
                        {204, "Igrave"},
                        {205, "Iacute"},
                        {206, "Icirc"},
                        {207, "Iuml"},
                        {208, "ETH"},
                        {209, "Ntilde"},
                        {210, "Ograve"},
                        {211, "Oacute"},
                        {212, "Ocirc"},
                        {213, "Otilde"},
                        {214, "Ouml"},
                        {215, "times"},
                        {216, "Oslash"},
                        {217, "Ugrave"},
                        {218, "Uacute"},
                        {219, "Ucirc"},
                        {220, "Uuml"},
                        {221, "Yacute"},
                        {222, "THORN"},
                        {223, "szlig"},
                        {224, "agrave"},
                        {225, "aacute"},
                        {226, "acirc"},
                        {227, "atilde"},
                        {228, "auml"},
                        {229, "aring"},
                        {230, "aelig"},
                        {231, "ccedil"},
                        {232, "egrave"},
                        {233, "eacute"},
                        {234, "ecirc"},
                        {235, "euml"},
                        {236, "igrave"},
                        {237, "iacute"},
                        {238, "icirc"},
                        {239, "iuml"},
                        {240, "eth"},
                        {241, "ntilde"},
                        {242, "ograve"},
                        {243, "oacute"},
                        {244, "ocirc"},
                        {245, "otilde"},
                        {246, "ouml"},
                        {247, "divide"},
                        {248, "oslash"},
                        {249, "ugrave"},
                        {250, "uacute"},
                        {251, "ucirc"},
                        {252, "uuml"},
                        {253, "yacute"},
                        {254, "thorn"},
                        {255, "yuml"},
                        {402, "fnof"},
                        {913, "Alpha"},
                        {914, "Beta"},
                        {915, "Gamma"},
                        {916, "Delta"},
                        {917, "Epsilon"},
                        {918, "Zeta"},
                        {919, "Eta"},
                        {920, "Theta"},
                        {921, "Iota"},
                        {922, "Kappa"},
                        {923, "Lambda"},
                        {924, "Mu"},
                        {925, "Nu"},
                        {926, "Xi"},
                        {927, "Omicron"},
                        {928, "Pi"},
                        {929, "Rho"},
                        {931, "Sigma"},
                        {932, "Tau"},
                        {933, "Upsilon"},
                        {934, "Phi"},
                        {935, "Chi"},
                        {936, "Psi"},
                        {937, "Omega"},
                        {945, "alpha"},
                        {946, "beta"},
                        {947, "gamma"},
                        {948, "delta"},
                        {949, "epsilon"},
                        {950, "zeta"},
                        {951, "eta"},
                        {952, "theta"},
                        {953, "iota"},
                        {954, "kappa"},
                        {955, "lambda"},
                        {956, "mu"},
                        {957, "nu"},
                        {958, "xi"},
                        {959, "omicron"},
                        {960, "pi"},
                        {961, "rho"},
                        {962, "sigmaf"},
                        {963, "sigma"},
                        {964, "tau"},
                        {965, "upsilon"},
                        {966, "phi"},
                        {967, "chi"},
                        {968, "psi"},
                        {969, "omega"},
                        {977, "thetasym"},
                        {978, "upsih"},
                        {982, "piv"},
                        {8226, "bull"},
                        {8230, "hellip"},
                        {8242, "prime"},
                        {8243, "Prime"},
                        {8254, "oline"},
                        {8260, "frasl"},
                        {8472, "weierp"},
                        {8465, "image"},
                        {8476, "real"},
                        {8482, "trade"},
                        {8501, "alefsym"},
                        {8592, "larr"},
                        {8593, "uarr"},
                        {8594, "rarr"},
                        {8595, "darr"},
                        {8596, "harr"},
                        {8629, "crarr"},
                        {8656, "lArr"},
                        {8657, "uArr"},
                        {8658, "rArr"},
                        {8659, "dArr"},
                        {8660, "hArr"},
                        {8704, "forall"},
                        {8706, "part"},
                        {8707, "exist"},
                        {8709, "empty"},
                        {8711, "nabla"},
                        {8712, "isin"},
                        {8713, "notin"},
                        {8715, "ni"},
                        {8719, "prod"},
                        {8721, "sum"},
                        {8722, "minus"},
                        {8727, "lowast"},
                        {8730, "radic"},
                        {8733, "prop"},
                        {8734, "infin"},
                        {8736, "ang"},
                        {8743, "and"},
                        {8744, "or"},
                        {8745, "cap"},
                        {8746, "cup"},
                        {8747, "int"},
                        {8756, "there4"},
                        {8764, "sim"},
                        {8773, "cong"},
                        {8776, "asymp"},
                        {8800, "ne"},
                        {8801, "equiv"},
                        {8804, "le"},
                        {8805, "ge"},
                        {8834, "sub"},
                        {8835, "sup"},
                        {8836, "nsub"},
                        {8838, "sube"},
                        {8839, "supe"},
                        {8853, "oplus"},
                        {8855, "otimes"},
                        {8869, "perp"},
                        {8901, "sdot"},
                        {8968, "lceil"},
                        {8969, "rceil"},
                        {8970, "lfloor"},
                        {8971, "rfloor"},
                        {9001, "lang"},
                        {9002, "rang"},
                        {9674, "loz"},
                        {9824, "spades"},
                        {9827, "clubs"},
                        {9829, "hearts"},
                        {9830, "diams"},
                        {34, "quot"},
                        {38, "amp"},
                        {60, "lt"},
                        {62, "gt"},
                        {338, "OElig"},
                        {339, "oelig"},
                        {352, "Scaron"},
                        {353, "scaron"},
                        {376, "Yuml"},
                        {710, "circ"},
                        {732, "tilde"},
                        {8194, "ensp"},
                        {8195, "emsp"},
                        {8201, "thinsp"},
                        {8204, "zwnj"},
                        {8205, "zwj"},
                        {8206, "lrm"},
                        {8207, "rlm"},
                        {8211, "ndash"},
                        {8212, "mdash"},
                        {8216, "lsquo"},
                        {8217, "rsquo"},
                        {8218, "sbquo"},
                        {8220, "ldquo"},
                        {8221, "rdquo"},
                        {8222, "bdquo"},
                        {8224, "dagger"},
                        {8225, "Dagger"},
                        {8240, "permil"},
                        {8249, "lsaquo"},
                        {8250, "rsaquo"},
                        {8364, "euro"}
                    };

            _entityValue = _entityName.ToDictionary(x => x.Value, x => x.Key);

            _maxEntitySize = 8 + 1; // we add the # char
        }

        #region Public Methods

        /// <summary>
        /// Replace known entities by characters.
        /// </summary>
        /// <param name="text">The source text.</param>
        /// <returns>The result text.</returns>
        public static string DeEntitize(string text)
        {
            if (text == null)
                return null;

            if (text.Length == 0)
                return text;

            StringBuilder sb = new StringBuilder(text.Length);
            ParseState state = ParseState.Text;
            StringBuilder entity = new StringBuilder(10);

            for (int i = 0; i < text.Length; i++)
            {
                switch (state)
                {
                    case ParseState.Text:
                        switch (text[i])
                        {
                            case '&':
                                state = ParseState.EntityStart;
                                break;

                            default:
                                sb.Append(text[i]);
                                break;
                        }
                        break;

                    case ParseState.EntityStart:
                        switch (text[i])
                        {
                            case ';':
                                if (entity.Length == 0)
                                {
                                    sb.Append("&;");
                                }
                                else
                                {
                                    if (entity[0] == '#')
                                    {
                                        string e = entity.ToString();
                                        try
 										{
											string codeStr = e.Substring(1).Trim().ToLower();
											int fromBase;
											if (codeStr.StartsWith("x"))
											{
												fromBase = 16;
												codeStr = codeStr.Substring(1);
											}
											else
											{
												fromBase = 10;
											}
											int code = Convert.ToInt32(codeStr, fromBase);
 											sb.Append(Convert.ToChar(code));
 										}
                                        catch
                                        {
                                            sb.Append("&#" + e + ";");
                                        }
                                    }
                                    else
                                    {
                                        // named entity?
                                        int code;
                                        if(_entityValue.TryGetValue(entity.ToString(), out code)==false) 
                                        {
                                            // nope
                                            sb.Append("&" + entity + ";");
                                        }
                                        else
                                        {
                                            // we found one
                                            sb.Append(Convert.ToChar(code));
                                        }
                                    }
                                    entity.Remove(0, entity.Length);
                                }
                                state = ParseState.Text;
                                break;

                            case '&':
                                // new entity start without end, it was not an entity...
                                sb.Append("&" + entity);
                                entity.Remove(0, entity.Length);
                                break;

                            default:
                                entity.Append(text[i]);
                                if (entity.Length > _maxEntitySize)
                                {
                                    // unknown stuff, just don't touch it
                                    state = ParseState.Text;
                                    sb.Append("&" + entity);
                                    entity.Remove(0, entity.Length);
                                }
                                break;
                        }
                        break;
                }
            }

            // finish the work
            if (state == ParseState.EntityStart)
            {
                sb.Append("&" + entity);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Clone and entitize an HtmlNode. This will affect attribute values and nodes' text. It will also entitize all child nodes.
        /// </summary>
        /// <param name="node">The node to entitize.</param>
        /// <returns>An entitized cloned node.</returns>
        public static HtmlNode Entitize(HtmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            HtmlNode result = node.CloneNode(true);
            if (result.HasAttributes)
                Entitize(result.Attributes);

            if (result.HasChildNodes)
            {
                Entitize(result.ChildNodes);
            }
            else
            {
                if (result.NodeType == HtmlNodeType.Text)
                {
                    ((HtmlTextNode) result).Text = Entitize(((HtmlTextNode) result).Text, true, true);
                }
            }
            return result;
        }


        /// <summary>
        /// Replace characters above 127 by entities.
        /// </summary>
        /// <param name="text">The source text.</param>
        /// <returns>The result text.</returns>
        public static string Entitize(string text)
        {
            return Entitize(text, true);
        }

        /// <summary>
        /// Replace characters above 127 by entities.
        /// </summary>
        /// <param name="text">The source text.</param>
        /// <param name="useNames">If set to false, the function will not use known entities name. Default is true.</param>
        /// <returns>The result text.</returns>
        public static string Entitize(string text, bool useNames)
        {
            return Entitize(text, useNames, false);
        }

        /// <summary>
        /// Replace characters above 127 by entities.
        /// </summary>
        /// <param name="text">The source text.</param>
        /// <param name="useNames">If set to false, the function will not use known entities name. Default is true.</param>
        /// <param name="entitizeQuotAmpAndLtGt">If set to true, the [quote], [ampersand], [lower than] and [greather than] characters will be entitized.</param>
        /// <returns>The result text</returns>
        public static string Entitize(string text, bool useNames, bool entitizeQuotAmpAndLtGt)
//		_entityValue.Add("quot", 34);	// quotation mark = APL quote, U+0022 ISOnum 
//		_entityName.Add(34, "quot");
//		_entityValue.Add("amp", 38);	// ampersand, U+0026 ISOnum 
//		_entityName.Add(38, "amp");
//		_entityValue.Add("lt", 60);	// less-than sign, U+003C ISOnum 
//		_entityName.Add(60, "lt");
//		_entityValue.Add("gt", 62);	// greater-than sign, U+003E ISOnum 
//		_entityName.Add(62, "gt");
        {
            if (text == null)
                return null;

            if (text.Length == 0)
                return text;

            StringBuilder sb = new StringBuilder(text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                int code = text[i];
                if ((code > 127) ||
                    (entitizeQuotAmpAndLtGt && ((code == 34) || (code == 38) || (code == 60) || (code == 62))))
                {
                    string entity = _entityName[code];
                    if ((entity == null) || (!useNames))
                    {
                        sb.Append("&#" + code + ";");
                    }
                    else
                    {
                        sb.Append("&" + entity + ";");
                    }
                }
                else
                {
                    sb.Append(text[i]);
                }
            }

            return sb.ToString();
        }

        #endregion

        #region Private Methods

        private static void Entitize(HtmlAttributeCollection collection)
        {
            foreach (HtmlAttribute at in collection)
            {
                at.Value = Entitize(at.Value);
            }
        }

        private static void Entitize(HtmlNodeCollection collection)
        {
            foreach (HtmlNode node in collection)
            {
                if (node.HasAttributes)
                    Entitize(node.Attributes);

                if (node.HasChildNodes)
                {
                    Entitize(node.ChildNodes);
                }
                else
                {
                    if (node.NodeType == HtmlNodeType.Text)
                    {
                        ((HtmlTextNode) node).Text = Entitize(((HtmlTextNode) node).Text, true, true);
                    }
                }
            }
        }

        #endregion

        #region Nested type: ParseState

        private enum ParseState
        {
            Text,
            EntityStart
        }

        #endregion
    }
}