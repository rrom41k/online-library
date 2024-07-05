FROM node:20.1.0 AS build
WORKDIR /app
COPY package.json /app/package.json
RUN npm install --only=prod
COPY ./src ./src
COPY ./public ./public
RUN npm run build

FROM nginx:1.16.0-alpine
COPY --from=build /app/build/ /nginx/static/
COPY nginx.conf /etc/nginx/nginx.conf:ro
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]