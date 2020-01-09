import { IPlayerMove } from "./player-move";

export interface IRound {
  id: number,
  gameId: number,  
  number: number,
  winnerName: string,
  winnerMove: string
  playersMoves: IPlayerMove[]
  showScore: boolean
}
