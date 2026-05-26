# Theming Guide

## Color Scheme

### Default (Cyberpunk Dark)

```csharp
// Colors.xaml
<Color x:Key="PrimaryColor">#9D4EDD</Color>         <!-- Violet -->
<Color x:Key="SecondaryColor">#0F0F0F</Color>       <!-- Almost Black -->
<Color x:Key="TertiaryColor">#1A1A2E</Color>        <!-- Dark Blue -->
<Color x:Key="AccentColor">#FF6B6B</Color>          <!-- Red (errors) -->
<Color x:Key="SuccessColor">#51CF66</Color>         <!-- Green (success) -->
<Color x:Key="TextPrimary">#FFFFFF</Color>          <!-- White -->
<Color x:Key="TextSecondary">#B0B0B0</Color>        <!-- Gray -->
<Color x:Key="NeonGlow">#00FFD9</Color>             <!-- Cyan -->
```

## Creating Custom Theme

### 1. Create Theme File

Create `themes/CustomTheme.xaml`:

```xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    
    <!-- Colors -->
    <Color x:Key="PrimaryColor">#Your Color</Color>
    <Color x:Key="BackgroundColor">#Your Color</Color>
    
    <!-- Brushes -->
    <SolidColorBrush x:Key="PrimaryBrush" Color="{StaticResource PrimaryColor}"/>
    
    <!-- Styles -->
    <Style TargetType="Button" BasedOn="{StaticResource BaseButtonStyle}">
        <Setter Property="Background" Value="{StaticResource PrimaryBrush}"/>
    </Style>
</ResourceDictionary>
```

### 2. Register Theme in App.xaml

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/Themes/CustomTheme.xaml"/>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

## Gradient Backgrounds

### Linear Gradient

```xml
<Border>
    <Border.Background>
        <LinearGradientBrush StartPoint="0,0" EndPoint="1,1">
            <GradientStop Color="#9D4EDD" Offset="0"/>
            <GradientStop Color="#0F0F0F" Offset="1"/>
        </LinearGradientBrush>
    </Border.Background>
</Border>
```

### Radial Gradient

```xml
<RadialGradientBrush Center="0.5,0.5" RadiusX="0.5" RadiusY="0.5">
    <GradientStop Color="#9D4EDD" Offset="0"/>
    <GradientStop Color="#0F0F0F" Offset="1"/>
</RadialGradientBrush>
```

## Effects

### Glass Morphism

```xml
<Border Background="#1A1A2E" Opacity="0.8">
    <Border.Effect>
        <BlurEffect Radius="10"/>
    </Border.Effect>
</Border>
```

### Neon Glow

```xml
<TextBlock Text="NEON" Foreground="#00FFD9">
    <TextBlock.Effect>
        <DropShadowEffect Color="#00FFD9" BlurRadius="20" ShadowDepth="0" Opacity="1"/>
    </TextBlock.Effect>
</TextBlock>
```

### Shadow Effect

```xml
<Button>
    <Button.Effect>
        <DropShadowEffect Color="#9D4EDD" BlurRadius="15" ShadowDepth="0" Opacity="0.5"/>
    </Button.Effect>
</Button>
```

## Animation Examples

### Fade In

```xml
<EventTrigger RoutedEvent="Loaded">
    <BeginStoryboard>
        <Storyboard>
            <DoubleAnimation
                Storyboard.TargetProperty="Opacity"
                From="0" To="1" Duration="0:0:0.5"/>
        </Storyboard>
    </BeginStoryboard>
</EventTrigger>
```

### Slide

```xml
<EventTrigger RoutedEvent="Loaded">
    <BeginStoryboard>
        <Storyboard>
            <ThicknessAnimation
                Storyboard.TargetProperty="Margin"
                From="0,-50,0,0" To="0,0,0,0" Duration="0:0:0.3"
                EasingFunction="QuadraticEase"/>
        </Storyboard>
    </BeginStoryboard>
</EventTrigger>
```

### Pulse

```xml
<Storyboard x:Key="PulseAnimation" RepeatBehavior="Forever">
    <DoubleAnimation
        Storyboard.TargetProperty="Opacity"
        From="1" To="0.5" Duration="0:0:1"
        AutoReverse="True"/>
</Storyboard>
```

## Font Customization

### Custom Font Registration

```xml
<FontFamily x:Key="PrimaryFont">pack://application:,,,/Fonts/#Segoe UI</FontFamily>
<FontFamily x:Key="MonoFont">pack://application:,,,/Fonts/#Consolas</FontFamily>
```

### Font Sizes

```xml
<System:Double x:Key="FontSizeLarge">20</System:Double>
<System:Double x:Key="FontSizeMedium">14</System:Double>
<System:Double x:Key="FontSizeSmall">12</System:Double>
```

## Responsive Design

### Adaptive Grid

```xml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*"/>
        <ColumnDefinition Width="300"/>
    </Grid.ColumnDefinitions>
</Grid>
```

### Adaptive Font Size

```csharp
public class ResponsiveFontConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var screenWidth = SystemParameters.PrimaryScreenWidth;
        
        if (screenWidth < 1366)
            return 12d;
        else if (screenWidth < 1920)
            return 14d;
        else
            return 16d;
    }
}
```

## Dark/Light Mode Toggle

```csharp
public void ToggleTheme(bool isDark)
{
    var app = Application.Current;
    var dict = new ResourceDictionary();
    
    if (isDark)
    {
        dict.Source = new Uri("pack://application:,,,/Themes/DarkTheme.xaml");
    }
    else
    {
        dict.Source = new Uri("pack://application:,,,/Themes/LightTheme.xaml");
    }
    
    app.Resources.MergedDictionaries.Clear();
    app.Resources.MergedDictionaries.Add(dict);
}
```

## Best Practices

1. **Use Resource Keys** - Always define colors, fonts, sizes as resources
2. **Consistency** - Apply same spacing, shadows, and effects throughout
3. **Accessibility** - Ensure sufficient contrast ratios (WCAG AA)
4. **Performance** - Avoid complex gradients on many controls
5. **Animation Duration** - Keep animations between 200-500ms
6. **Responsive** - Test on 1366x768, 1920x1080, and 2560x1440
