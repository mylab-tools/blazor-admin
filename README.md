# MyLab.BlazorAdmin

[![NuGet Version and Downloads count](https://buildstats.info/nuget/MyLab.BlazorAdmin)](https://www.nuget.org/packages/MyLab.ABlazorAdmin)

Ознакомьтесь с последними изменениями в [журнале изменений](/CHANGELOG.md).

## Обзор

`MyLab.BlazorAdmin` - каркас одностраничного приложения в стиле "админики" на платформе [ASP.NET Core Blazor](https://learn.microsoft.com/ru-ru/aspnet/core/blazor/?view=aspnetcore-7.0) в варианте WebAssembly . Предоставляет сервисы и готовые компоненты, возможности расширения и кастомизации для разработки клиентского веб-приложения. Такая "админка" представляет собой клиентское веб-приложение для работы в личном кабинете, к особенностям которого относятся:

* вход/выход (аутентификация);
* отображение текущего пользователя;
* отображение навигации по разделам верхнего уровня;
* похожие по структуре разделы;
* схожие компоненты в дизайне страниц.

## Компоненты

### Блок описания

`DescriptionBlock` - компонент на базе [Bootstrap Alert](https://getbootstrap.com/docs/5.0/components/alerts/). Имеет цветовой стиль [secondary](https://getbootstrap.com/docs/5.0/customize/color/). Предназначен для отображения описательной информации. Имеет крестик для сворачивания. Сворачивается в ссылку, с помощью которой его опять можно развернуть. Состояние компонента хранится в локальном хранилище браузера.

 ```html
 <DescriptionBlock Id="my-description" CollapsedStateTitle="Подробнее...">
     Это описание будет видно в развёрнутом виде.
 </DescriptionBlock>
 ```

## Страница админки

Класс вложенной страницы админки `AdminNestedPage` - базовый класс для страниц админки. Предоставляет расширенный функционал для интеграции в расстановку интерфейса адмиинки `AdminLayout`.

### Навигационные кнопки

У `AdminLayout` есть панель навигации текущей страницы. Для размещения навигационных кнопок на этой панели, страница должна зарегистрировать навигационные ссылки с помощью защищённого метода `AddNavigation` базового класса `AdminNestedPage`.

### Cтатус-Алерт

`AdminLayout` позволяет размещать информационный блок в начале страницы. Этот блок предназначен реализован на базе [Bootstrap Alert](https://getbootstrap.com/docs/5.0/components/alerts/) и отражает текущее состояние страницы или сообщает о статусе последней операции. 

Установка информационного блока осуществляется методом `PutStatusAlert`.  

## Диалоги

Управление диалогами осуществляется через сервис `IDialogService`.

## Сообщения

Сообщения реализованы на базе диалогов и доступны через методы расширения сервиса `IDialogService`

