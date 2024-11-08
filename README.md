# Teste_67Bits
Este projeto foi desenvolvido como parte de um teste técnico para a posição de Desenvolvedor Unity na empresa 67 Bits. O objetivo foi criar uma cena interativa com um personagem que realiza diversas ações de acordo com as demandas apresentadas, assemelhando-se a um jogo mobile hipercasual.

## Demonstração do Jogo
O jogo possui todas as funcionalidades implementadas conforme solicitado.

### Funcionalidades Implementadas
**Movimentação do Personagem**
O personagem se move com uma animação de corrida e rotaciona para sempre estar virado na direção de movimento.

**Integração com o Novo Input System da Unity**
O jogo foi desenvolvido utilizando o novo Input System, o que permite comandos por touchscreen, teclado e mouse, e gamepads. O personagem pode ser controlado tanto com toques na tela quanto pelas teclas WASD ou através de controles.

**Sistema de Ataque e Ragdoll**
O jogador pode socar inimigos, ativando um sistema de respawn que gera um novo inimigo no local e transforma o inimigo abatido em um ragdoll. O inimigo abatido pode ser coletado e adicionado à pilha nas costas do jogador.

**Sistema de Empilhamento com Inércia**
Cada inimigo abatido se junta à pilha nas costas do jogador. Ao movimentar-se, os inimigos empilhados acompanham o movimento com um leve atraso para simular inércia. Não há uso de joints ou animações pré-prontas — toda a movimentação é feita por scripts que ajustam valores em tempo de execução. A pilha tem um limite de capacidade que aumenta conforme o jogador sobe de nível.

**Sistema de Venda e Acúmulo de Ouro**
O jogador pode "vender" a pilha de inimigos em uma área específica, ganhando um valor fixo de 200 de ouro por inimigo vendido.

**Compra de Experiência e Progressão de Nível**
Com o ouro acumulado, o jogador pode comprar experiência (XP) em uma área de compra e venda. Ao acumular XP suficiente, o personagem sobe de nível, o que aumenta a capacidade de empilhamento e altera a cor e a escala do personagem. A cada nível, o personagem recebe buffs como aumento de velocidade e maior força no soco, além de um incremento no número máximo de inimigos na pilha.

**Opções de UI**
O projeto inclui duas opções de UI: uma UI dinâmica (semelhante ao jogo Gym Beast), onde o jogador clica em qualquer área da tela para mover o personagem, e uma UI fixa, com o joystick travado no canto da tela e um botão de soco ao lado. Ambas as opções mantêm a lógica do jogo enquanto variam as funcionalidades e modos de interação.

### Como Rodar o Projeto
Para rodar o projeto, clone o repositório e abra-o no Unity. Certifique-se de estar utilizando uma versão compatível (Unity 2023 ou superior).
De preferencia, use a versão 2023.1.15f1
