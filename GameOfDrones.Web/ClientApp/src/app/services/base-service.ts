import { environment } from '../../environments/environment';

export class BaseService {

  baseUrl = environment.baseUrl;
  gameUrl = environment.gameUrl;
  roundUrl = environment.roundUrl;
  rankingUrl = environment.rankingUrl;
  logsUrl = environment.logsUrl;

  constructor() { }
}
