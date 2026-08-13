using System.Net.Sockets;

namespace UltimaSaveEditor.Ultima5;


/// <summary>
//0209  Grapple
//020A Magic Carpets
//020D  Amulet of Lord British
//020E  Crown of Lord British
//020F  Sceptre of Lord British
//0210  Shard of Falsehood
//0211  Shard of Hatred
//0212  Shard of Cowardice
//0214  Spy Glass
//0215  HMS Cape Plans
//0216  Sextant
//0217  Pocket Watch
//0218  Black Badge
//0219  Sandalwood Box
/// </summary>
public enum QuestItemType
{
    Grapple,
    Amulet,
    Crown,
    MagicCarpet,
    PocketWatch,
    Sceptre,

    ShardOfHatred,
    ShardOfCowardice,
    ShardOfFalsehood,

    HmsCapePlans,

    Sextant,
    SpyGlass,

    BlackBadge,
    SandalwoodBox
}