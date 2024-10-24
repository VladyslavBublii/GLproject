const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:8503';

const PROXY_CONFIG = [
  {
    context: [
      "/privacy",
      "/login/signin",
      "/registration/registration",
      "/store/get",
      "/cart/getBasket",
      "/cart/add",
      "/cart/remove",
      "/order/makeOrder",
      "/order/getByUserId",
    ],
    target: target,
    secure: false, 
    changeOrigin: true, 
    headers: {
      Connection: 'Keep-Alive'
    }
  }
];

module.exports = PROXY_CONFIG;
