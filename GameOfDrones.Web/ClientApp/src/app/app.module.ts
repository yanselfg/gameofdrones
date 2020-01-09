import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
// Services
import { GameService } from './services/game-service';
import { RankingService } from './services/ranking-service';
import { LogService } from './services/log-service';
// Components
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { GameComponent } from './components/game/game.component';
import { RankingComponent } from './components/ranking/ranking.component';
import { LogsComponent } from './components/logs/logs.component';
import { APP_BASE_HREF } from '@angular/common';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    GameComponent,
    RankingComponent,
    LogsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    NgxPaginationModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'ranking', pathMatch: 'full' },
      { path: 'game', component: GameComponent, pathMatch: 'full' },
      { path: 'ranking', component: RankingComponent, pathMatch: 'full' },
      { path: 'logs', component: LogsComponent, pathMatch: 'full' }
    ])
  ],
  providers: [
    { provide: APP_BASE_HREF, useFactory: getBaseUrl },
    GameService,
    RankingService,
    LogService
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }

export function getBaseUrl() {
  return environment.baseUrl;
}
