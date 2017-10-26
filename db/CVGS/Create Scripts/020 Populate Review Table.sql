INSERT INTO REVIEW( MemberId
                  , GameId
                  , ReviewText
                  , Rating )
            Values( 4          -- Doug
                  , 1          -- Zelda
                  , 'A very good game'
                  , 5 )
                , ( 5          -- Kendall
                  , 2          -- Halo 3
                  , 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus at elit efficitur, varius ex at, faucibus urna. Aliquam erat volutpat. Quisque ac lorem semper, ullamcorper lorem eget, dictum lorem. Nam in nisi volutpat, ultrices erat vitae, aliquet leo. Sed sit amet elementum odio, at dictum nunc. Duis congue bibendum mauris eget egestas. Donec tincidunt, enim eu convallis porttitor, eros dui semper nibh, sit amet varius diam ante eu libero. Vivamus ultrices lectus dui, sed efficitur neque euismod a. Cras ac cursus neque, sed consectetur velit. Phasellus lacus massa, fringilla ut lorem eget, condimentum posuere turpis. Ut ac sem id quam ultrices fermentum nec eget lectus. Maecenas porta, nisi sed viverra egestas, urna sem pulvinar velit, vitae rhoncus eros arcu ultricies elit. Etiam sit amet lorem in enim molestie pulvinar ac quis arcu. Cras suscipit, est ac molestie fermentum, eros odio porttitor tortor, sed fringilla tellus mi ac ipsum. Ut quis elit arcu. Sed quis pretium volutpat.'
                  , 4 )
                , ( 6          -- Tristan
                  , 5          -- Spyro the Dragon
                  , 'I like the dragon'
                  , 5 )
                , ( 6          -- Lucas
                  , 1          -- Zelda
                  , ''
                  , 4 )
                , ( 3          -- Ezio         
                  , 4          -- GTA:V
                  , 'Too violent'
                  , 2 )
                , ( 6          -- Tristan         
                  , 4          -- GTA:V
                  , ''
                  , 4 )
                , ( 4          -- Doug         
                  , 4          -- GTA:V
                  , NULL
                  , 2 )
                , ( 6          -- Lucas
                  , 2          -- Halo 3
                  , 'This is a Halo game. It is the third Halo game.'
                  , 4 );
GO