# CopilotTest2
This is a test project for GitHub Copilot AI and is purely used for learning purposes.

## Overview of the project
This project is created from a .NET MAUI application template using Visual Studio 2022 Pro. 
The objective of this app in its current scope is to be able to pull a randomly generated flower image from https://randomwordgenerator.com/picture.php and display it. It is also intended to have the ability to download this freely available image to the local device.
The target platform for this app is Android.

## Project Structure
Essentially, the project is structured as follows:
- The main page of the app is `MainPage.xaml` (The View) which is a `ContentPage` that contains an `Image` element.
- The code-behind file for the main page is `MainPage.xaml.cs` which contains the logic for binding the view model, `MainPageViewModel.cs`, to the xaml.
- This view model contains the logic for calling on `FlowerService.cs` to fetch the image from the web, and then the view model displays it in the view.
- Both the view model and the service use `Flower.cs` as a Model for the flower image.

## Current Status
Currently, to keep things simple, I am attempting to fetch/locate a random image from the /Resources/Images folder. This feature is broken and my current task is fixing it.
The "Download Image" function is not yet working but an attempt has been made to implement it.
Once the image fetching/locating has been fixed, I will then attempt to fix the implementation functionality of downloading the image to the local device.
