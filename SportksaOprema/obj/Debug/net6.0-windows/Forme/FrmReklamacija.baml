   M S B A M L   `   `   `  ����I  ESportksaOprema, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:SportksaOprema.FormeSportksaOprema.Forme  5   	   )    #SportksaOprema.Forme.FrmReklamacija   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:SportksaOprema.Forme     5   	     � Title$  Dodaj/Izmeni reklamaciju��5   	   $	��300��6*   $	��300��67   .��5	      � .+�5
   
   �� $��Opis garancije:��6   $
��Left=�6*   $��	41,22,0,0q�6E   $	��Top=�6X   �� 5   
   -   5         ��Name$ txtOpis��5      $
��Left=�6#   $��23��6>   $��
135,23,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $�� ��6r   $	��Top=�6z   $	��120��6�   �� 5   
   $��
Zaposleni:��6   $
��Left=�6%   $��	66,52,0,0q�6@   $	��Top=�6S   �� 5   
   $��Kupac:��6   $
��Left=�6!   $��	84,82,0,0q�6<   $	��Top=�6O   �� 5   
   -   5         ��Name$ 
btnSacuvaj��5      $��Sačuvaj��6%   $
��Left=�67   $��
30,196,0,0q�6R   $	��Top=�6f   $��75��6~   6�   �� 5   
   -   5        $ 	btnOtkazi��5      $��Otkaži��6$   $
��Left=�65   $��180,196,0,0q�6P   $	��Top=�6e   $��75��6}   6�   �� 5   
   -   5         ��Name$ cbZaposleni��5      $
��Left=�6(   $��
135,52,0,0q�6C   $	��Top=�6W   $	��120��6o    �� DisplayMemberPath$	 ime��6{    �� SelectedValuePath$ zaposleniID��6�   �� 5   
   -   5        $ cbKupac��5      $
��Left=�6$   $��
135,82,0,0q�6?   $	��Top=�6S   $	��120��6k   $ imeKupca��6w   $ kupacID��6�   �� 5   
   $��Uvazena:��6   $
��Left=�6#   $��
72,112,0,0q�6>   $	��Top=�6R   �� 5   
   -   5         ��Name$ checkUvazena��5      $��CheckBox��6)   $
��Left=�6<   $��135,118,0,0q�6W   $	��Top=�6l   �� 5   
   $	��ID:��6   $
��Left=�6   $��105,138,0,0q�69   $	��Top=�6N   �� 5   
   -   5        $ txtID��5      $
��Left=�6!   $��135,142,0,0q�6<   $
 Wrap=�6Q   $�� ��6e   $	��Top=�6m   $	��120��6�   5      5      