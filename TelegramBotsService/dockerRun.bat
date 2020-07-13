set imageName=mongo
@echo -+ stop %imageName% +-
set containerName=my-mongo
docker stop %containerName%

@echo -+ run %imageName% +-
docker run --rm -d -p 27017:27017 --name %containerName% %imageName%

:@echo -+ get %imageName% ip +-
:set ip=docker inspect -f "{{ .NetworkSettings.Networks.bridge.IPAddress }}" %containerName%
:@echo ip=%ip%

set tbsImageName=mihail1orlov/tbs
@echo -+ start tbs +-
docker run --rm -it --name tbs %tbsImageName%