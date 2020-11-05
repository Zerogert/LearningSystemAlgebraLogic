# README #

This README would normally document whatever steps are necessary to get your application up and running.

### What is this repository for? ###

Asp.Net Core Rest API for the Speecheer project
* Version
* [Learn Markdown](https://bitbucket.org/tutorials/markdowndemo)

### How do I get set up? ###

* Summary of set up
* Configuration
* Dependencies
* Database configuration
* How to run tests
* Deployment instructions

### Contribution guidelines ###

* Writing tests
* Code review
* Other guidelines

### Who do I talk to? ###

* Repo owner or admin
* Other community or team contact

#### Deployment instructions ###
1. Создание instance в Amazon
    - Кликаем Launch instance
    - Отмечаем галочку на чекбоксе Free tier only
    - Выбираем Ubuntu Server 18.04 LTS (HVM), SSD Volume Type (64-bit (x86))
    - на выборе типа оставляем по умолчанию (t2.micro)
    - на Configure Instance тоже все по умолчанию
    - далее резмер хранилиша оставляем как есть
    - далее  Configure Security Group выбираем Defaut
    - Вводим Key pair name например Speecheer и скачиваем его
1. Подключение к instance
    - Скачиваем и устанавливаем PuTTY
    - запускаем PuTTY Key Generator
    - нажимаем кнопку load и выбираем загруженный файл например Speecheer.pem
    - нажимаем Save private Key и сохраняем
    - Далее в командной строке переходим в директорию где лежит сохраненый на предыдущем шаге файл
    - в амазоне смотрм строку подклчюения для этого в списке инстансов выбираем наш и кликаем на кнопку коннект там будет строка подключения например
      ````
      ssh -i "Speek.pem" ubuntu@ec2-34-241-161-134.eu-west-1.compute.amazonaws.com
      ````
    - вводим в консоль
1. Установка и настройка Dotnet core
 	- Для этого последовательно прописываем команды
 	
          wget https://packages.microsoft.com/config/ubuntu/19.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
          sudo dpkg -i packages-microsoft-prod.deb
          sudo apt-get update
          sudo apt-get install apt-transport-https
          sudo apt-get update
          sudo apt-get install dotnet-sdk-3.1

1. Установка и настройка MySQL
   - Для установки прописываем последовательно команды
      ````shell
      sudo apt update
      sudo apt install mysql-server
      ````
    - Для первоначальной настройки
      ````
      sudo mysql_secure_installation
      ````
      - После нас попросят придумать и ввести пароль
    - Подклчаемся к MySQl  коммандой
       ````
       sudo mysql -p
       ````
      - вводим пароль
    - создание пользователя вводим
      ``CREATE USER 'username'@'localhost' IDENTIFIED BY 'password';``
    - Добавляем ему привилегий
      ``GRANT ALL PRIVILEGES ON *.* TO 'username'@'localhost' WITH GRANT OPTION;``
    - создаем БД
      ``CREATE DATABASE databasename;``
    - выходим
      ``EXIT;``
    
1. Клонирование и настройка проекта
      - Создаем и переходи в директорию где будет лежать проекта
          ```
          sudo mkdir /var/www/
          cd /var/www
          ```
      - клонируем проект
      `sudo git clone https://Zerogert@bitbucket.org/speecheer/rest-api.git`
      - меняем ветку
          ```shell
          cd rest-api
          sudo git checkout origin/develop
          ```
      - настраиваем
         ```shell
          cd  Speecheer-rest-api
           sudo nano appsettings.json
         ```
          - Заменяем данные в строке подклчюения
              `"DefaultConnection":"server=localhost;port=3306;database=databasename;uid=username;password=password"`
       - сохраняем
       - При необходимости делаем тоже самое в appsettings.Development.json

   - сборка проекта и запуск
        ```shell
        sudo dotnet build
        sudo dotnet publish
        sudo dotnet run
        ```
1. Установка и настройка nginx
      ```shell
      cd ~
      sudo apt-get update
      sudo apt-get install nginx
      sudo nano /etc/nginx/sites-available/default
      ```
   - Заменяем весь текст на это. Где сервер нейм заменить своим доменным именем
      ```
      server {
              listen          *:443;
              server_name   ec2-34-241-161-134.eu-west-1.compute.amazonaws.com *.ec2-34-241-161-134.eu-west-1.compute.amazonaws.com;
              include   /etc/nginx/ssl.conf;
              location / {
                  proxy_pass         http://localhost:5000;
                  proxy_http_version 1.1;
                  proxy_set_header   Upgrade $http_upgrade;
                  proxy_set_header   Connection keep-alive;
                  proxy_set_header   Host $host;
                  proxy_cache_bypass $http_upgrade;
                  proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
                  proxy_set_header   X-Forwarded-Proto $scheme;
              }
          }
      ```
    - Создадим общий файл конфигурации SSL:
       `sudo nano /etc/nginx/ssl.conf`
       - с содержимым
       ```
       ssl       on;
       ssl_protocols     SSLv3 TLSv1;
       ssl_certificate       /etc/nginx/ssl/cert.pem;
       ssl_certificate_key   /etc/nginx/ssl/cert.key;
       ```
    - Создадим папку где будем хранить сертификаты:
     ` sudo mkdir /etc/nginx/ssl`
    - Перейдем в эту папку и сгенерируем сертификат:
      ```shell
      cd /etc/nginx/ssl
      sudo openssl req -new -x509 -days 9999 -nodes -out cert.pem -keyout cert.key
      ```
      - При генерации вас попросят указать некоторые данные, так как мы создаём сертификат для себя то заполнять их не обязательно.
      `sudo systemctl restart nginx`
1. Запуск проекта
    ```shell
    cd /var/www/rest-api/Speecheer-rest-api
    sudo dotnet run
    ```
1. Finish
