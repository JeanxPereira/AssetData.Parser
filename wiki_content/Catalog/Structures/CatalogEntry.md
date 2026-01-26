# CatalogEntry
**Size:** `0x28`
**Count:** `0x7`

## Structure
| Offset | DataType | Name |
| :--- | :--- | :--- |
| `0x00` | `charptr` | **assetNameWType** |
| `0x08` | `int64` | **compileTime** |
| `0x10` | `int` | **version** |
| `0x14` | `uint` | **typeCrc** |
| `0x18` | `uint` | **dataCrc** |
| `0x1C` | `charptr` | **sourceFileNameWType** |
| `0x20` | `Array<charptr>` | **tags** |

---
## Reference
> Used by:

- [`Catalog`](../Structures/Catalog)

