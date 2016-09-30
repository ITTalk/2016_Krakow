# Program napisany w ramach warsztatów ITTALK 2016

## Wymagania i dodatki
### Windows
* dot net SDK - https://go.microsoft.com/fwlink/?LinkID=827524 lub jako dodatek do Visual Studio.
* docker dla Windows 10 - https://download.docker.com/win/stable/InstallDocker.msi
* docker dla Windows < 10 - https://github.com/docker/toolbox/releases/download/v1.12.0/DockerToolbox-1.12.0.exe

### Linux 
* dotnet  
```bash
sudo sh -c 'echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/dotnet-release/ xenial main" > /etc/apt/sources.list.d/dotnetdev.list' 
sudo apt-key adv --keyserver apt-mo.trafficmanager.net --recv-keys 417A0893 sudo apt-get update 
sudo apt-get install dotnet-dev-1.0.0-preview2-003131
```
* docker - https://docs.docker.com/engine/installation/linux/


## Zawartość
* ITTalk_Nuget_Version - wersja aplikacji  z bibliotekami nuget
* ITTalk_Project_Version - wersja aplikacji ze źródłami
* ItTalkServer - serwer

## ITTalk Server
Najpierw trzeba uruchomić ItTalkServer.
Aby upewnić się, że serwer poprawnie się buduje, wykonaj:

```bash
cd ItTalkServer
dotnet restore 
dotnet build 
```

Następnie należy stworzyć obraz:

```bash
docker build -t ittalkserver .
```

Upewnij się, że został stworzony i znajduje się na liście :
```bash
docker images
```

Ostatnim krokiem jest uruchomienie kontenera z opcją przekazania portów:
```bash
docker run -it -p "5000:5000" ittalkserver
```

W przeglądarce pod adresem **192.168.99.100:5000** dostępna jest strona serwera.

## ITTalk_Nuget_Version
Aby uruchomić wersję nuget i wygenerowac obrazek.
W folderze  ITTalk_Nuget_Version wykonaj polecenia: 
```bash
cd ITTalk
dotnet build 
dotnet run
dotnet restore 
```
w tym momencie aplikacja powinna sie uruchomić.
Aby spakowac aplikację w kontener nalezy wykonac poniższe polecenie bezpośrednio w katalogu ITTalk_Nuget_Version
```bash
docker build . -t ittalkclient
```
**ittalkclient** - jest to nazwa kontenera, może byc dowolna. W odróżnieniu od wersji na pokazie, w tym przykładzie kontener nie będzie publikowany do registry.

```bash
docker images
```
po wykonaniu tego polecenie będziemy widzieć utworzone obrazy.

```bash
docker run nazwa_obrazu numer_obrazka
docker run ittalkclient 1 
```
jako numer obrazka może zostać podana dowolna liczba od 0 do 9.
Po ruchomieniu na stronie serwera powinien pojawić się obrazek.



## ITTalk_Project_Version
Ta wersja w odróżnieniu od wersji nuget nie posiada wybudowanych cześniej plików nuget, ani pliku NuGet.Config.
Zamiast tego, wszystkie źródła budowane są od zera
W folderze  ITTalk_Project_Version wykonaj polecenia: 
```bash
dotnet restore 
cd ITTalk
dotnet build 
dotnet run
```
w tym momencie aplikacja powinna sie uruchomić.
Aby spakowac aplikację w kontener nalezy wykonac poniższe polecenie bezpośrednio w katalogu ITTalk_Project_Version
```bash
docker build . -t ittalkclient
```
**ittalkclient** - jest to nazwa kontenera, może byc dowolna. W odróżnieniu od wersji na pokazie, w tym przykładzie kontener nie będzie publikowany do registry.

```bash
docker images
```
po wykonaniu tego polecenie będziemy widzieć utworzone obrazy.

```bash
docker run nazwa_obrazu numer_obrazka
docker run ittalkclient 1 
```
jako numer obrazka może zostać podana dowolna liczba od 0 do 9.
Po ruchomieniu na stronie serwera powinien pojawić się obrazek.
