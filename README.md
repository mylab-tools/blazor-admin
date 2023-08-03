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

### Сворачиваемое описание

`CollapsingDescription` - компонент на базе [Bootstrap ALert](https://getbootstrap.com/docs/5.0/components/alerts/). Имеющий цветовой стиль [secondary](https://getbootstrap.com/docs/5.0/customize/color/). Предназначен для отображения описательной информации. Имеет крестик для сворачивания. Сворачивается в ссылку, с помощью которой его опять можно развернуть. Состояние компонента хранится в локальном хранилище браузера.

 ```html
 <CollapsingDescription Id="my-description" CollapsedStateTitle="Подробнее...">
     Это описание будет видно в развёрнутом виде.
 </CollapsingDescription>
 ```

## Диалоги

Управление диалогами осуществляется через сервис `IDialogService`.

## Сообщения

Сообщения реализованы на базе диалогов и доступны через методы расширения сервиса `IDialogService`