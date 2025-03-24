# Tugas Besar 1 Strategi Algoritma 2024/2025 
## Pemanfaatan Algoritma Greedy dalam pembuatan bot permainan Robocode Tank Royale
4 Alternatif Bot:
1. ProtokolKesehatan
ProtokolKesehatan adalah bot yang dirancang untuk Robocode Tank Royale dengan strategi berbasis algoritma greedy yang fokus pada penjagaan jarak dari musuh. Bot ini selalu bergerak menuju posisi di arena yang memiliki risiko minimum. Risiko dikalkulasi berdasarkan berdasarkan energi, sudut, dan jarak dari musuh yang masih hidup.
 
3. WallHugger.exe
WallHugger.exe adalah bot permainan Robocode Tank Royale yang menerapkan strategi berupa gabungan dari dua strategi Greedy yang berbeda. Strategi Greedy yang pertama adalah nearest wall. Jadi pada awal ronde, bot akan mencari tembok yang paling dekat dengan posisinya dan bergerak ke tembok tersebut. Strategi ini bertujuan untuk menghindari daerah tengah yang cenderung ramai dan dengan bergerak ke salah satu tembok, bot tidak perlu memperhatikan musuh di belakang (karena belakangnya tembok).

4. SBS
SBS adalah bot yang diprogram untuk permainan Robocode Tank Royale dengan menggunakan strategi algoritma greedy yang fokus pada penyerangan efisien dan penyerangan balik ketika terserang bot lain. Pada awal ronde permainan, bot akan menghindari wilayah tengah dan segera menuju tembok/dinding batas permainan yang ditetapkan. Terdapat strategi untuk tiga kondisi utama, yaitu yang pertama adalah ketika melakukan penyerangan umum setelah memindai musuh. Strategi ini berfungsi untuk menghemat energi peluru berbasis jarak dari bot SBS ke bot musuh yang dipindai. Kondisi kedua adalah ketika terserang peluru di mana bot SBS akan menyerang balik musuh sesuai arah peluru tersebut. Kondisi ketiga adalah ketika menghantam bot musuh di mana bot SBS akan langsung menyerang maksimal pada arah bot musuh lalu menjauh.

5. YangPentingNembak
YangPentingNembak adalah bot yang dirancang untuk Robocode Tank Royale dengan strategi greedy algorithm yang berfokus pada penyerangan agresif dan pemindaian musuh secara maksimal. Bot ini mengutamakan tindakan instan untuk memaksimalkan damage per detik (memaksimalkan poin), seperti menembak dengan firepower maksimal (3.0) dan memutar radar 360° terus-menerus untuk mendeteksi musuh.

## Struktur Folder
*Workspace* ini terdiri dari:  
- **`src/`**:  
  - `main-bot/`: Bot utama dengan strategi Greedy optimal  
  - `alternative-bots/`: 3 bot alternatif dengan heuristic berbeda  
- **`doc/`**: Laporan resmi dalam format PDF  

## Program Requirements & Dependencies
Program ini dibuat dengan Visual Studio Code. Berikut merupakan beberapa *dependencies* yang diperlukan untuk menjalankan program.
- **Java 17 atau lebih baru**
- **.NET SDK 7.0+**  
- **Game Engine Modifikasi**: [Robocode Tank Royale (versi asisten)](https://github.com/robocode-dev/tank-royale)  

### Instalasi Java (Jika Belum Terinstal)
Windows: Unduh dan instal dari [`Oracle JDK`](https://www.oracle.com/java/technologies/downloads/?er=221886) atau gunakan[`Adoptium OpenJDK`](https://adoptium.net/).

Linux/macOS:
```sh
sudo apt install openjdk-17-jdk  # Untuk Ubuntu/Debian  
brew install openjdk@17          # Untuk macOS dengan Homebrew  
```
Detail lebih lanjut mengenai java dependency dalam VSCode dapat diakses di [sini](https://github.com/microsoft/vscode-java-dependency#manage-dependencies).

### Instalasi .NET SDK  
**Windows**:  
Unduh dari [dotnet.microsoft.com](https://dotnet.microsoft.com/)  

**Linux/macOS**:  
```bash  
# Ubuntu/Debian  
sudo apt-get update && sudo apt-get install -y dotnet-sdk-7.0  

# macOS (Homebrew)  
brew install dotnet@7  
```


## Author
| Nama | NIM | Kelas IF2211
| --- | --- | --- |
| Yosef Rafael Joshua  | 13522133 | K3 |
| Asybel B.P. Sianipar | 15223011 | K1 |
| Ignacio Kevin Alberiann | 15223090 | K1 |

## Referensi
- [Spesifikasi Tugas Besar 1 Stima 2024/2025](https://docs.google.com/document/d/14MCaRiFGiA6Ez5W8-OLxZ9enXyENcep7AzSH6sUHKM8/edit?tab=t.0)
- [Dokumentasi Robocode Tank Royale](https://robocode-dev.github.io/tank-royale/?spm=a2ty_o01.29997173.0.0.1820c921OkPQ2P)
- [.NET Documentation](https://dotnet.microsoft.com/en-us/learntocode?spm=a2ty_o01.29997173.0.0.1820c921OkPQ2P)
