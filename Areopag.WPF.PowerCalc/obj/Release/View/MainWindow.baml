   M S B A M L   `   `   `  ���� P  LAreopag.WPF.PowerCalc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Areopag.WPF.PowerCalc.ViewModels Areopag.WPF.PowerCalc.ViewModels  +    %Areopag.WPF.PowerCalc.View.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     =vm.clr-namespace:Areopag.WPF.PowerCalc.ViewModels     -     �Name$  My_main_window�� � Title$S MРасчет параметров дозировочного агрегата��$	��880��$	��880�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � DataContext :  4Areopag.WPF.PowerCalc.ViewModels.MainWindowViewModel   � 	Resources  �� 
TargetType �� Triggers P� Property	 P� Value
 �� Property �� Value �� RelativeSource �� Path��%�   '��       �� " �� P�  �@=�HasError	 . . ��� 
 �@�ToolTip ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent��� 
 � ��
 �  ��.���  � ColumnDefinitions �� $	��87* ��� $
��133* �.+��� $��0,26,0,0q����	#FFACACAC$y�Hidden=�$��2��.@��� -    ��Name$ One_two_head_tab��$f��`Расчет агрегата с одинаковыми головками и приводами��$��-2,-2,-99,0q� �� 
IsSelected . ����  �� EndPoint$	 0,1&� �� 
StartPoint$	 0,0&�.���  � Color$ 	#FFF0F0F0�� � Offset$ 0Q�� $ 	#FFE5E5E5��$ 1Q�o� -    o�Name$ Menu_bar��$
��Left=�$��25��$	��Top=�$
��auto��
���   �.@�m� -    m�Name$ MyMenu1��$��Файл��$��25�����	#FFE5E5E5
���   �$	��160��$��Center=�$��Center=�$��17��.@�m� -   $ Save_us_item��$O��IЭкспортировать результаты расчета в Excel��$��18��m� -   $ About��$��О программе��$��19�� m� Command �� *AboutCommand+m� -   $ Exit_menu_item��$��
Выход��$��20�� �� *ExitCommand+m� -   $ MyMenu2��$1��+Дополнительные функции��$��25�����	#FFE5E5E5
���   �$	��160��$��Center=�$��Center=�$��21��.@�m� -	   $ PGA_volume_calc��$P��JРассчитать требуемый агрегату объем ПГА��$��22�� �� *LaunchPGAApplicationCommand+� 
�������$��3,30,3,3q� J� RenderTransformOrigin$ 0.622,0.552&�$��2�� � RowDefinitions �� �� �� �� �� �� �� �� �� �� �� �� �� ��  �� �� �� �� �� �� �� �� �� �� .+��� -
    ��Name$ 
GB_general��$:��4Основные параметры агрегата��$��0��$��0��$��4��$��3��$��5,5,5,5q�
���   �.��� ���	#FFE5E5E5 �� �� ��  �� �� �� �� �� �� �� �� .+��� -    ��Name$ 
P2_textbox�� �� TextWrapping$
 Wrap=�$��1��$��6��$��1��$��2��$��1��$��5,5,5,5q� �� TextAlignment$ Center=����� *Aggregate_P2+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=��� -   $ 
P1_textbox��$
 Wrap=�$��2��$��6��$��2��$��2��$��1��$��5,5,5,5q�$ Center=����� *Aggregate_P1+$ PropertyChanged=�$ TwoWay=��� -   $ Qapacity_textbox��$
 Wrap=�$ 2.244,5.476&�$��0��$��6��$��0��$��2��$��1��$��5,5,5,5q�$ Center=�.������ $ Aggregate_qapacity�$ PropertyChanged=�  �� NotifyOnValidationError  . $ TwoWay=�! �� ValidationRules! 5  /System.Windows.Controls.DataErrorValidationRule  .���� -   " ��Name$" P2_label��$C��=Давление на выходе P2, кгс/см2 (изб.)��$��SemiCondensed&�$��0��$��1��$��5��$��1��$��5,5,5,5q��� -   $" P1_label��$A��;Давление на входе P1, кгс/см2 (абс.)��$��0��$��2��$��6��$��1��$��5,5,5,5q��� -   $" Qapacity_label��$A��;Номинальная подача агрегата, л/ч��$��0��$��0��$��5��$��1��$��5,5,5,5q��� -   $ GB_efficienty��$F��@Показатели эффективности агрегата��$��0��$��3��$��4��$��3��$��5,5,5,5q�
���   �.��� ���	#FFE5E5E5 �� �� ��  �� �� �� �� �� �� �� �� .+��� -   $" vol_eff_label��$*��$η объемный агрегата��$��0��$��0��$��4��$��1��$��5,5,5,5q��� -   $ Vol_eff_textbox��$
 Wrap=�$��3��$��6��$��0��$��2��$��1��$��5,5,5,5q�$ Center=����� *Aggr_vol_efficienty+# �� StringFormat$# {0:f2}��$ PropertyChanged=�$ TwoWay=��� -   $" mech_eff_label��$!��η мех. агрегата��$��0��$��2��$��4��$��1��$��5,5,5,5q��� -   $ Mech_eff_textbox��$
 Wrap=�$��4��$��6��$��2��$��2��$��1��$��5,5,5,5q�$ Center=����� *Aggr_mech_efficienty+$# {0:f2}��$ PropertyChanged=�$ TwoWay=��� -   $" hydr_eff_label��$)��#η гидравл. агрегата��$��0��$��1��$��4��$��1��$��5,5,5,5q��� -   $ Hydr_eff_textbox��$
 Wrap=�$��3��$��6��$��1��$��2��$��1��$��5,5,5,5q�$ Center=����� *Aggr_hydr_efficienty+$# {0:f2}��$ PropertyChanged=�$ TwoWay=��� -   $ GB_construction��$7��1Конструктивная реализация��$��0��$��6��$��4��$��5��$��5,5,5,5q�
���   �.��� ���	#FFE5E5E5 �� �� �� �� �� �� ��  �� �� �� �� �� �� �� �� .+��� $E��?Количество головок (рабочих камер)��$��0��$��0��$��6��$��1��$��2,2,2,2q��� -   $ Qantity_of_heads_textbox��$
 Wrap=�$��6��$��6��$��0��$��2��$��1��$��5,5,5,5q�$ Center=����� *Heads_qantity+$ PropertyChanged=�$ TwoWay=��� -   $" Strokes_label��$K��EЧисло двойных ходов плунжера в минуту��$��0��$��1��$��6��$��1��$��2,2,2,2q��� -   $ Strokes_textbox��$
 Wrap=�$��8��$��6��$��1��$��2��$��1��$��5,5,5,5q�$ Center=����� *	Strokes+$ PropertyChanged=�$ TwoWay=��� -   $" Stroke_label��$6��0Величина хода плунжера, мм��$��0��$��2��$��6��$��1��$��2,2,2,2q��� -   $ Stroke_lenght_textbox��$
 Wrap=�$��9��$��6��$��2��$��2��$��1��$��5,5,5,5q�$ Center=����� *Stroke_length+$ PropertyChanged=�$ TwoWay=��� -   $ ��Name$"$ EnterPlungerDiameterCheckbox��$=��7Ручной ввод диаметра плунжера��$��10��$��0��$��3��$��6��$��1��$��2,2,2,2q�% d� 	IsChecked% �� *EnterPlungerDiam+$ TwoWay=��� -   $" Pl_Diam_label��$+��%Диаметр плунжера, мм��$��0��$��4��$��4��$��1��$��2,2,2,2q����� " ��.��� 
 � y�$ Hidden=� �� & �� Binding& �� ' �� ElementName$"' EnterPlungerDiameterCheckbox��$ 	IsChecked�( �� Value( True.a��� 
 � y�$ Visible=��� -    $ PlungerDiamTextbox��$
 Wrap=�$��7��$��6��$��4��$��2��$��1��$��5,5,5,5q�$ Center=����� *ManualPlungerDiam+$ PropertyChanged=�$ TwoWay=�.������ " ��.��� 
 � y�$ Hidden=� �� & �� $"' EnterPlungerDiameterCheckbox��$ 	IsChecked�( True.a��� 
 � y�$ Visible=�.���� -!   $!$ Enter_safety_coeff_checkbox��$H��BРучной ввод коэффициента запаса эДв��$��10��$��0��$��5��$��6��$��1��$��2,2,2,2q�% �� *	EnterSC+$ TwoWay=��� -"   $" Safety_coeff_label��$)��#Коэффициент запаса��$��0��$��6��$��4��$��1��$��2,2,2,2q����� " ��.��� 
 � y�$ Hidden=� �� & �� $!' Enter_safety_coeff_checkbox��$ 	IsChecked�( True.a��� 
 � y�$ Visible=��� -#   $ Safety_coeff_textbox��$
 Wrap=�$��7��$��6��$��6��$��2��$��1��$��5,5,5,5q�$ Center=����� *Safety_coefficient+$# {0:f1}��$ PropertyChanged=�$ TwoWay=�.������ " ��.��� 
 � y�$ Hidden=� �� & �� $!' Enter_safety_coeff_checkbox��$ 	IsChecked�( True.a��� 
 � y�$ Visible=�.���� -$   ) ��Name$) Show_drives_row��$.��(Показать ряд приводов��$��2��$��12��$��2��$��1��$��5,5,5,5q�
���   �$��14������ *ShowDrivesCommand+�� -%   $) Show_pl_row_btn��$0��*Показать ряд плунжеров��$��2��$��11��$��2��$��1��$��5,5,5,5q�
���   �$��13������ *ShowPlungersCommand+�� -&   $) 
ED_row_btn��$$��Показать ряд эДв��$��2��$��14��$��2��$��1��$��5,5,5,5q�
���   �$��15������ *ShowElectricDrivesCommand+�� -'   $) Result_button��$%��Выполнить расчет��$��0��$��11��$��2��$��3��$��5,5,5,5q�$��15��$��16(����#D3D3D3
���   ����� *ResultCommand+&   System.Windows.Controls.DataGrid  -(   *  Name$* 
ResultGrid��$��4��$��0��$��6��$��14��$��5,14,5,5q����� *	Results+