import { IRound } from "./round";
import { IPlayer } from "./player";

export interface IGame {
  id: number,
  rounds: IRound[],
  currentRound: IRound,
  players: IPlayer[],
  winnerName: string,
  showWinner: boolean
}
