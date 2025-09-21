# PostBlog
Пример API для сайта-блога по типу X и т.п.

Рекомендуется поднимать сервисы через <i>docker-compose up</i>. :whale:

Для корректной работы с эндпоинтами необходимо создать аккаунт, для этого регистрируем его по следующему эндпоинту в AuthService:
<img width="1507" height="565" alt="image" src="https://github.com/user-attachments/assets/0a9759d7-d5b4-474b-8ed7-d88e7ae9ffea" style="border-radius: 20px;"/>

Далее, используя эндпоинт логина, можем получить JWT токен. В AuthService он переноситься в Cookie.
<img width="1411" height="531" alt="image" src="https://github.com/user-attachments/assets/d3066eaa-0a00-4f69-ac49-75988b20ee37" style="border-radius: 20px;"/>

А уже в PostService необходимо вставить его вручную.
<img width="1467" height="660" alt="image" src="https://github.com/user-attachments/assets/c5056c12-b28c-496c-907b-84db973d1eee" style="border-radius: 20px;" />
<img width="642" height="279" alt="image" src="https://github.com/user-attachments/assets/ee099f1d-0418-41e5-a75f-ba61b0603ec6" style="border-radius: 20px;"/>
<hr>
It is recommended to run the services using <i>docker-compose up</i> :whale:

To work correctly with the endpoints, you need to create an account. To do this, register via the following endpoint in the AuthService:
<img width="1507" height="565" alt="image" src="https://github.com/user-attachments/assets/0a9759d7-d5b4-474b-8ed7-d88e7ae9ffea" style="border-radius: 20px;"/>

Next, using the login endpoint, you can obtain a JWT token. In the AuthService, it is stored in a Cookie.
<img width="1411" height="531" alt="image" src="https://github.com/user-attachments/assets/d3066eaa-0a00-4f69-ac49-75988b20ee37" style="border-radius: 20px;"/>

Then, in the PostService, you need to insert it manually.
<img width="1467" height="660" alt="image" src="https://github.com/user-attachments/assets/c5056c12-b28c-496c-907b-84db973d1eee" style="border-radius: 20px;" />
<img width="642" height="279" alt="image" src="https://github.com/user-attachments/assets/ee099f1d-0418-41e5-a75f-ba61b0603ec6" style="border-radius: 20px;"/>










