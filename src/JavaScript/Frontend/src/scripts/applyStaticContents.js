import http, { request } from 'http';
import fs from 'node:fs';

const readJsonData = async (filePath) => {
    try {
        const data = await fs.promises.readFile(filePath, 'utf8');
        return JSON.parse(data);
    } catch (error) {
        console.error('Error reading JSON data:', error);
        throw error;
    }
};

const createRequestData = (jsonData) => {
    const requestData = [];

    Object.keys(jsonData).forEach((key) => {
        const [contentKey, language] = key.split('|');
        if (typeof jsonData[key] === 'string') {
            jsonData[key] = jsonData[key].replace(/[\u007F-\uFFFF]/g, (char) => {
                return "\\u" + ("0000" + char.charCodeAt(0).toString(16)).slice(-4);
            });
        }
        requestData.push({
            key: contentKey,
            languageData: [
                {
                    data: jsonData[key],
                    language,
                }
            ],
        });
    });
    
    return JSON.stringify(Array.from(requestData));
};

const sendBulkContentRequest = (dataString) => {
    const options = {
        hostname: 'localhost',
        port: 6901,
        path: '/api/Content/AddBulkContentWithKey',
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Content-Length': dataString.length,
        },
    };

    const request = http.request(options, (response) => {
        let data = '';
        response.on('data', (chunk) => {
            data += chunk.toString();
        });

        response.on('end', () => {
            const operation = JSON.parse(data);
            console.log(operation);
            console.log(`Operation was ${operation.isSuccess ? 'successful' : 'not successful'}`);

        });

        response.on('error', (error) => {
            console.error('Error in response:', error);
        });
    });

    request.write(dataString);
    request.end();
};

(async () => {
    try {
        const jsonData = await readJsonData('./src/defaultStaticContents.json');
        const dataString = createRequestData(jsonData);
        sendBulkContentRequest(dataString);
    } catch (error) {
        console.error('Error reading or processing data:', error);
    }
})();
