import { Component, OnInit } from '@angular/core';
import { LogService } from '../../services/log-service';
import { ILogItem } from '../../interfaces/log-item';

@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html'
})
export class LogsComponent implements OnInit {
  public logs: ILogItem[];
  public config: any;

  constructor(private logsService: LogService) {    
  }

  ngOnInit() {
    this.logsService.logs().subscribe(
      data => {
        this.logs = data;
        this.config = {
          itemsPerPage: 10,
          currentPage: 1,
          totalItems: this.logs.length
        };
      }
    );
  }

  pageChanged(event) {
    this.config.currentPage = event;
  }
}
