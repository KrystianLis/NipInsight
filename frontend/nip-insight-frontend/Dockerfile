FROM node:14 AS build

RUN mkdir /app
WORKDIR /app

ENV PATH /app/node_modules/.bin:$PATH
ENV API_URL=http://localhost:5020/nip/

COPY package-lock.json /app/package-lock.json
COPY package.json /app/package.json

COPY . /app

RUN npm install

# start app
CMD ["npm", "start"]

EXPOSE 3000