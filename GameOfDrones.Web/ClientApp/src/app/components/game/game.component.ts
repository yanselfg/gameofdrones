import { Component, OnInit } from '@angular/core';
//import { Location } from '@angular/common'; 
import { Router } from '@angular/router';
import { GameService } from '../../services/game-service';
import { IGame } from '../../interfaces/game';
//import { IRound } from 'src/app/interfaces/round';


@Component({
  selector: 'app-game-form',
  templateUrl: './game.component.html',
  //styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

  public moves: string[]
  public game: IGame;

  constructor(private router: Router, private gameService: GameService) {
  }

  ngOnInit() {
    this.onInitializeModel();
  }

  onSubmitGame() {
    this.gameService.game(this.game).subscribe(
      data => {
        this.game = data
      }
    );
  }

  onSubmitMove() {
    if (this.game.currentRound.playersMoves[0].moveName != '') {
      this.game.currentRound.playersMoves[0].selected = true;
    }

    if (this.game.currentRound.playersMoves[1].moveName != '') {
      this.game.currentRound.playersMoves[1].selected = true;
    }

    if (this.game.currentRound.playersMoves[0].selected && this.game.currentRound.playersMoves[1].selected) {
      this.gameService.round(this.game).subscribe(
        data => {
          this.game = data;
        }
      );
    }
  }

  onPlayAgain() {
    this.onInitializeModel();
  }

  onInitializeModel(): void {
    this.moves = ['Rock', 'Paper', 'Scissors'];
    this.game = {
      id: 0,
      showWinner: false,
      winnerName: "",
      currentRound: {
        id: 0,
        gameId: 0,
        number: 0,
        winnerName: "",
        winnerMove: "",
        showScore: false,
        playersMoves: [
          {
            moveName: "",
            playerName: "",
            selected: false
          },
          {
            moveName: "",
            playerName: "",
            selected: false
          }
        ]
      },
      rounds: [],
      players: [
        {
          id: 0,
          name: ""
        },
        {
          id: 0,
          name: ""
        }
      ]
    };
  }

  //////// NOT SHOWN IN DOCS ////////

  // Reveal in html:
  //   Name via form.controls = {{showFormControls(heroForm)}}
  //showFormControls(form: any) {
  //  return form && form.controls['name'] &&
  //    form.controls['name'].value; // Dr. IQ
  //}

  /////////////////////////////

}
