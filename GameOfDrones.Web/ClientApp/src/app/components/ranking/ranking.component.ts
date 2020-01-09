import { Component, OnInit } from '@angular/core';
import { RankingService } from '../../services/ranking-service';
import { IRankingItem } from '../../interfaces/ranking-item';

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html'
})
export class RankingComponent implements OnInit {
  public ranking: IRankingItem[];
  public config: any;

  constructor(private rankingService: RankingService) {    
  }

  ngOnInit() {
    this.rankingService.ranking().subscribe(
      data => {
        this.ranking = data;
        this.config = {
          itemsPerPage: 10,
          currentPage: 1,
          totalItems: this.ranking.length
        };
      }
    );
  }

  pageChanged(event) {
    this.config.currentPage = event;
  }
}
