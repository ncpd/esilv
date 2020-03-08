FROM node:alpine

WORKDIR /usr/app

COPY . /usr/app

RUN apk --no-cache add --virtual builds-deps build-base python && npm install

EXPOSE 5000

CMD [ "npm", "start" ]