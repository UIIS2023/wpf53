   M S B A M L   `   `   `  ����I  ESportksaOprema, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:SportksaOprema.FormeSportksaOprema.Forme  5   	   &     SportksaOprema.Forme.FrmKupovina   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:SportksaOprema.Forme     5   	     � Title$  Dodaj/Izmeni kupovinu��5   	   $	��400��6'   $	��400��64   .��5	      � $��
0,0,-21,-6q�6   .+�5
   
   �� $��Popust��6   $
��Left=�6!   $��
109,85,0,0q�6<   $	��Top=�6P   �� 5   
   $��Broj racuna:��6   $
��Left=�6'   $��	83,55,0,0q�6B   $	��Top=�6U   �� 5   
   -   5         ��Name$ txtBrojRacuna��5      $
��Left=�6)   $��23��6D   $��
161,58,0,0q�6P    �� TextWrapping$
 Wrap=�6d   $�� ��6x   $	��Top=�6�   $	��120��6�   �� 5   
   $��Vrsta kupovine:��6   $
��Left=�6*   $��	65,23,0,0q�6E   $	��Top=�6X   �� 5   
   -   5        $ txtVrstaKupovine��5      $
��Left=�6,   $��23��6G   $��
161,24,0,0q�6S   $
 Wrap=�6g   $�� ��6{   $	��Top=�6�   $	��120��6�   �� 5   
   $��Iznos popusta:��6   $
��Left=�6)   $��
69,111,0,0q�6D   $	��Top=�6X   �� 5   
   -   5        $ txtIznosPopusta��5      $
��Left=�6+   $��23��6F   $��161,111,0,0q�6R   $
 Wrap=�6g   $�� ��6{   $	��Top=�6�   $	��120��6�   �� 5   
   $��Datum��6   $
��Left=�6    $��110,137,0,0q�6;   $	��Top=�6P   �� 5   
   $��Kupac:��6   $
��Left=�6!   $��	105,0,0,0q�6<   $��Center=�6O   �� 5   
   $��Oprema:��6   $
��Left=�6"   $��
94,206,0,0q�6=   $	��Top=�6Q   �� 5   
   $��
Zaposleni:��6   $
��Left=�6%   $��
86,234,0,0q�6@   $	��Top=�6T   �� 5   
   $��
Garancija:��6   $
��Left=�6%   $��
88,261,0,0q�6@   $	��Top=�6T   �� 5   
   -   5         ��Name$ 
btnSacuvaj��5      $��Sačuvaj��6%   $
��Left=�67   $��105,354,0,0q�6R   $	��Top=�6g   $��75��6   6�   �� 5   
   -   5        $ 	btnOtkazi��5      $��Otkaži��6$   $
��Left=�65   $��200,354,0,0q�6P   $	��Top=�6e   $��76��6}   6�   �� 5   
   -   5         ��Name$ cbKupac��5      $
��Left=�6$   $��	161,0,0,0q�6?   $��Center=�6R   $	��120��6m    �� DisplayMemberPath$ imeKupca��6y    �� SelectedValuePath$ kupacID��6�   �� 5   
   -   5        $ cbOprema��5      $
��Left=�6%   $��161,208,0,0q�6@   $	��Top=�6U   $	��120��6m   $
 opis��6y   $ opremaID��6�   �� 5   
   -   5        $ cbZaposleni��5      $
��Left=�6(   $��161,236,0,0q�6C   $	��Top=�6X   $	��120��6p   $	 ime��6|   $ zaposleniID��6�   �� 5   
   -	   5        $ cbGarancija��5      $
��Left=�6(   $��161,263,0,0q�6C   $	��Top=�6X   $	��120��6p   $ trajanje��6|   $ garancijaID��6�   �� 5   
   -
   5         ��Name$ checkPopust��5      $��CheckBox��6(   $
��Left=�6;   $��
161,91,0,0q�6V   $	��Top=�6j   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dpDatum��5      $
��Left=�6&   $��161,139,0,0q�6A   $	��Top=�6V   �� 5   
   -   5        $ txtID��5      $
��Left=�6!   $��161,301,0,0q�6<   $
 Wrap=�6Q   $�� ��6e   $	��Top=�6m   $	��120��6�   �� 5    
   $	��ID:��6   $
��Left=�6   $��112,297,0,0q�69   $	��Top=�6N   5!      5"      